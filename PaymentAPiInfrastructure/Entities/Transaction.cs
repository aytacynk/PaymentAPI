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
    public class Transaction
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int BankId { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public StatusOptions Status { get; set; }
        public string OrderReference { get; set; }
        public DateTime TransactionDate { get; set; }
        public ICollection<TransactionDetail> TransactionDetails { get; set; }
    }
}
