using PaymentAPiInfrastructure.Contract;
using PaymentAPiInfrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentAPiInfrastructure.Impelementation
{
    public class YapiKredi : Bank
    {
        public override void Pay(Transaction transaction)
        {
            // YapiKredi-özgü implementasyon
        }

        public override void Cancel(Transaction transaction)
        {
            // YapiKredi-özgü implementasyon
        }

        public override void Refund(Transaction transaction)
        {
            // YapiKredi-özgü implementasyon
        }
    }
}
