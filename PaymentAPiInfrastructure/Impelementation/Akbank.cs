using PaymentAPiInfrastructure.Contract;
using PaymentAPiInfrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentAPiInfrastructure.Impelementation
{
    public class Akbank : Bank
    {
        public override void Pay(Transaction transaction)
        {
            // Akbank’a özgü implementasyon

        }

        public override void Cancel(Transaction transaction)
        {
            // Akbank’a özgü implementasyon

        }

        public override void Refund(Transaction transaction)
        {
            // Akbank’a özgü implementasyon

        }
    }
}
