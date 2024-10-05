using PaymentAPiInfrastructure.Impelementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentAPiInfrastructure.Contract
{
    public class BankFactory
    {
        public static Bank? GetBank(int? bankId)
        {
            switch (bankId)
            {
                case 1:
                    return new Akbank();
                case 2:
                    return new Garanti();
                case 3:
                    return new YapiKredi();
                default:
                    return null;
            }
        }
    }
}