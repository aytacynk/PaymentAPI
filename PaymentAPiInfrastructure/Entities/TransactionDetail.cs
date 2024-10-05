using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PaymentAPiInfrastructure.Enum.Enum;

namespace PaymentAPiInfrastructure.Entities
{
    public class TransactionDetail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid TransactionId { get; set; }
        public TransactionType TransactionType { get; set; } // İşlem tipi: Sale, Refund, Cancel
        public StatusOptions? Status { get; set; } // İşlem durumu: Success, Fail
        public decimal? Amount { get; set; } // İşlem miktarı
    }
}
