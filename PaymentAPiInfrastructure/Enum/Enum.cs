using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentAPiInfrastructure.Enum
{
    public class Enum
    {
        public enum StatusOptions
        {
            Success,
            Fail
        }
        public enum TransactionType
        {
            Sale,
            Refund,
            Cancel
        }
    }
}
