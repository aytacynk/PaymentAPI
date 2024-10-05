using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentAPiInfrastructure.Entities
{
    public class TransactionDetail
    {
        public Guid Id { get; set; }
        public Guid TransactionId { get; set; }
        public string TransactionType { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
    }
}
