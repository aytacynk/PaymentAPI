using PaymentAPiInfrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentAPiInfrastructure.Contract
{
    public abstract class Bank
    {
        public virtual void Pay(Transaction transaction) { }
        public virtual void Cancel(Transaction transaction) { }
        public virtual void Refund(Transaction transaction) { }
    }
}
