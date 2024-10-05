using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Models;
using PaymentAPiInfrastructure;
using PaymentAPiInfrastructure.Contract;
using PaymentAPiInfrastructure.Entities;
using static PaymentAPiInfrastructure.Enum.Enum;

namespace PaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly PaymentDbContext _paymentContext;

        public TransactionsController(PaymentDbContext context)
        {
            _paymentContext = context;
        }

        // Yeni Bir Ödeme Ekleme İşlemi
        [HttpPost("pay")]
        public async Task<IActionResult> Pay([FromBody] Transaction transaction)
        {
            var bank = GetBankOrDefault(transaction.BankId);
            if (bank == null)
            {
                return NotFound();
            }

            try
            {
                // Bankanın Pay metodunu çağır
                bank.Pay(transaction);

                // Veritabanına ekle

                await _paymentContext.Transactions.AddAsync(transaction);
                await _paymentContext.SaveChangesAsync();

                return Ok(transaction);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Hatalı durum için uygun bir yanıt döner

            }
        }

        // İptal Etme İşlemi
        [HttpPost("cancel")]
        public async Task<IActionResult> Cancel([FromBody] Transaction transaction)
        {
            var bank = GetBankOrDefault(transaction.BankId);
            if (bank == null)
            {
                return NotFound();
            }

            // İlgili işlemi veritabanında bulma işlemi
            var existingTransaction = await GetTransactionByIdAsync(transaction.Id);

            if (existingTransaction == null)
            {
                return NotFound(); // İşlem bulunamazsa
            }

            try
            {
                // İptal işlemini gerçekleştir kurala göre kontrol edilir
                bank.Cancel(existingTransaction);

                _paymentContext.Transactions.Update(existingTransaction);
                await _paymentContext.SaveChangesAsync();

                return Ok(existingTransaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Hatalı durum için uygun bir yanıt döner
            }
        }

        // Geri Ödeme İşlemi
        [HttpPost("refund")]
        public async Task<IActionResult> Refund([FromBody] Transaction transaction)
        {
            var bank = GetBankOrDefault(transaction.BankId);
            if (bank == null)
            {
                return NotFound();
            }

            // İlgili işlemi veritabanında bul
            var existingTransaction = await GetTransactionByIdAsync(transaction.Id);

            if (existingTransaction == null)
            {
                return NotFound(); // İşlem bulunamazsa
            }

            try
            {
                // İade işlemini gerçekleştir kurala göre kontrol edilir
                bank.Refund(existingTransaction);

                _paymentContext.Transactions.Update(existingTransaction);
                await _paymentContext.SaveChangesAsync();

                return Ok(existingTransaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Hatalı durum için uygun bir yanıt döner
            }
        }

        // Raporlama İşlemi
        [HttpGet("report")]
        public async Task<IActionResult> Report([FromQuery] ReportFilter filter)
        {
            var query = _paymentContext.Transactions.AsQueryable();

            // Banka ID'sine göre filtreleme
            if (filter.BankId.HasValue)
            {
                query = query.Where(t => t.BankId == filter.BankId.Value);
            }

            // İşlem durumuna göre filtreleme (Success, Fail)
            if (filter.Status.HasValue)
            {
                query = query.Where(t => t.Status == filter.Status.Value);
            }

            // Sipariş referansına göre filtreleme
            if (!string.IsNullOrEmpty(filter.OrderReference))
            {
                query = query.Where(t => t.OrderReference == filter.OrderReference);
            }

            // Tarih aralığına göre filtreleme
            if (filter.StartDate.HasValue)
            {
                query = query.Where(t => t.TransactionDate >= filter.StartDate.Value);
            }

            // Son Tarih ve öncesine göregöre filtreleme
            if (filter.EndDate.HasValue)
            {
                query = query.Where(t => t.TransactionDate <= filter.EndDate.Value);
            }

            var result = await query.ToListAsync();

            return Ok(result);
        }

        private Bank? GetBankOrDefault(int? bankId)
        {
            return BankFactory.GetBank(bankId);
        }

        private async Task<Transaction> GetTransactionByIdAsync(Guid transactionId)
        {
            return await _paymentContext.Transactions
                .Include(t => t.TransactionDetails) // TransactionDetails'ı dahil eder
                .FirstOrDefaultAsync(t => t.Id == transactionId);
        }

    }
}
