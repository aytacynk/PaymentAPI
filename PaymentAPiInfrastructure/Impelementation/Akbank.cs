using PaymentAPiInfrastructure.Contract;
using PaymentAPiInfrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PaymentAPiInfrastructure.Enum.Enum;

namespace PaymentAPiInfrastructure.Impelementation
{
    public class Akbank : Bank
    {
        public override void Pay(Transaction transaction)
        {
            // Gelen isteğe göre Sale tipi bir işlem oluştur
            transaction.TransactionDate = DateTime.UtcNow; // İşlem tarihini ayarla
            transaction.Status = StatusOptions.Success; // Başarılı bir işlem durumu ayarla

            // TransactionDetail nesnesi oluştur
            var transactionDetail = new TransactionDetail
            {
                Id = Guid.NewGuid(), // Yeni bir ID oluştur
                TransactionId = transaction.Id, // Yabancı anahtar
                TransactionType = TransactionType.Sale, // İşlem tipi 'Sale' olarak ayarlanır
                Amount = transaction.TotalAmount, // İşlem miktarı
                Status = transaction.Status // İşlem durumu
            };

            // Detayı işlemin detayları listesine ekle
            if (transaction.TransactionDetails == null)
            {
                transaction.TransactionDetails = new List<TransactionDetail>();
            }

            transaction.TransactionDetails.Add(transactionDetail); // Detayı listeye ekle
        }

        public override void Cancel(Transaction transaction)
        {
            // İşlem aynı gün içinde iptal edilmelidir
            if (transaction.TransactionDate.Date != DateTime.Today)
            {
                throw new Exception("İşlem aynı gün içinde iptal edilmelidir.");
            }

            // Pay işleminin yapılmış olup olmadığını kontrol et
            if (transaction.Status != StatusOptions.Success)
            {
                throw new Exception("İptal işlemi yalnızca başarılı bir Pay işleminden sonra yapılabilir.");
            }

            // İptal işlemi için transaction detayını güncelle
            foreach (var detail in transaction.TransactionDetails)
            {
                detail.Status = StatusOptions.Fail; // İptal durumunu güncelle
                detail.TransactionType = TransactionType.Cancel; // İşlem tipini 'Cancel' olarak ayarla
                detail.Amount = 0; // İptal işleminde miktarı sıfırla
            }

            // Net miktarı ve durumu güncelle
            transaction.NetAmount = 0; // İptal işleminde net miktar genellikle sıfırdır
            transaction.Status = StatusOptions.Fail; // İptal durumu için statüyü güncelle
        }

        public override void Refund(Transaction transaction)
        {
            // Pay işleminin yapılmış olup olmadığını kontrol et
            if (transaction.Status != StatusOptions.Success)
            {
                throw new Exception("İade işlemi yalnızca başarılı bir Pay işleminden sonra yapılabilir.");
            }

            // İşlem tarihi üzerinden bir gün geçmiş mi kontrol et
            if (transaction.TransactionDate.Date > DateTime.Today.AddDays(-1))
            {
                throw new Exception("İade işlemi için işlem tarihinden en az 1 gün geçmiş olmalıdır.");
            }

            // İade işlemi için transaction detayını güncelle
            foreach (var detail in transaction.TransactionDetails)
            {
                // İade işlemi için detayları güncelle
                detail.Status = StatusOptions.Success; // İade durumunu günceller
                detail.TransactionType = TransactionType.Refund; // İşlem tipini 'Refund' olarak ayarlar

                // Miktarı negatif yaparak iade işlemini simüle eder
                // Miktarı boş değilse negatif yap
                detail.Amount = detail.Amount.HasValue ? -Math.Abs(detail.Amount.Value) : (decimal?)null; 
            }

            // Net miktarı ve durumu güncelle
            transaction.NetAmount = 0; // İade işleminde net miktar genellikle sıfırdır
            transaction.Status = StatusOptions.Fail; // İade durumu için statüyü günceller
        }

    }
}
