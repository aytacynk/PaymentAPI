using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentAPiInfrastructure.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public int BankId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal NetAmount { get; set; }
        public string Status { get; set; }
        public string OrderReference { get; set; }
        public DateTime TransactionDate { get; set; }
        public ICollection<TransactionDetail> TransactionDetails { get; set; }
    }
}
