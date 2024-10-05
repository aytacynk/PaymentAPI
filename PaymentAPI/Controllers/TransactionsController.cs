using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Models;
using PaymentAPiInfrastructure;
using PaymentAPiInfrastructure.Contract;
using PaymentAPiInfrastructure.Entities;

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

        [HttpPost("pay")]
        public async Task<IActionResult> Pay([FromBody] Transaction transaction)
        {
            var bank = BankFactory.GetBank(transaction.BankId);

            if (bank == null)
            {
                return NotFound();
            }

            bank.Pay(transaction);
            await _paymentContext.Transactions.AddAsync(transaction);
            await _paymentContext.SaveChangesAsync();
            return Ok(transaction);
        }

        [HttpPost("cancel")]
        public async Task<IActionResult> Cancel([FromBody] Transaction transaction)
        {
            var bank = GetBankOrDefault(transaction.BankId);

            if (bank == null)
            {
                return NotFound();
            }

            bank.Cancel(transaction);
            _paymentContext.Transactions.Update(transaction);
            await _paymentContext.SaveChangesAsync();
            return Ok(transaction);
        }

        [HttpPost("refund")]
        public async Task<IActionResult> Refund([FromBody] Transaction transaction)
        {
            var bank = GetBankOrDefault(transaction.BankId);

            if (bank == null)
            {
                return NotFound();
            }

            bank.Refund(transaction);

            _paymentContext.Transactions.Update(transaction);
            await _paymentContext.SaveChangesAsync();

            return Ok(transaction);
        }

        [HttpGet("report")]
        public async Task<IActionResult> Report([FromQuery] ReportFilter filter)
        {
            var query = _paymentContext.Transactions.AsQueryable();

            if (filter.BankId.HasValue)
            {
                query = query.Where(t => t.BankId == filter.BankId.Value);
            }

            if (!string.IsNullOrEmpty(filter.Status))
            {
                query = query.Where(t => t.Status == filter.Status);
            }

            if (!string.IsNullOrEmpty(filter.OrderReference))
            {
                query = query.Where(t => t.OrderReference == filter.OrderReference);
            }

            if (filter.StartDate.HasValue)
            {
                query = query.Where(t => t.TransactionDate >= filter.StartDate.Value);
            }

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

    }
}
