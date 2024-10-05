using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentAPiInfrastructure.Contract
{
    public class PaymentDbContextFactory : IDesignTimeDbContextFactory<PaymentDbContext>
    {
        public PaymentDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PaymentDbContext>();

            // Bağlantı dizesini buraya yazın
            var connectionString = "server=localhost;database=payment_db;user=root;password=123456";

            // MariaDB versiyonunu ayarlayın
            var serverVersion = new MariaDbServerVersion(new Version(10, 3, 22)); // Kullandığınız MariaDB sürümünü buraya yazın

            optionsBuilder.UseMySql(connectionString, serverVersion, mySqlOptions =>
            {
                mySqlOptions.EnableRetryOnFailure(); // İsteğe bağlı, hata durumunda tekrar denemek için
            });

            return new PaymentDbContext(optionsBuilder.Options);
        }
    }
}
