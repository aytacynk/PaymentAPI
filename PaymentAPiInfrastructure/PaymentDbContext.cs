using Microsoft.EntityFrameworkCore;
using PaymentAPiInfrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentAPiInfrastructure
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> dbcontextoption) : base(dbcontextoption)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Üretim ortamında bu ayarı kaldırmalısınız
                optionsBuilder.UseMySql("Server=myServerAddress;Database=myDataBase;User=myUsername;Password=myPassword;",
                    ServerVersion.AutoDetect("Server=myServerAddress;Database=myDataBase;User=myUsername;Password=myPassword;"));
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>().ToTable("Transaction");
            modelBuilder.Entity<TransactionDetail>().ToTable("TransactionDetail");

        }
    }
}