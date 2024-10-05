﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaymentAPiInfrastructure;

#nullable disable

namespace PaymentAPiInfrastructure.Migrations
{
    [DbContext(typeof(PaymentDbContext))]
    partial class PaymentDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PaymentAPiInfrastructure.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("BankId")
                        .HasColumnType("int");

                    b.Property<decimal?>("NetAmount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("OrderReference")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal?>("TotalAmount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Transaction", (string)null);
                });

            modelBuilder.Entity("PaymentAPiInfrastructure.Entities.TransactionDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<decimal?>("Amount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("char(36)");

                    b.Property<int>("TransactionType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransactionId");

                    b.ToTable("TransactionDetail", (string)null);
                });

            modelBuilder.Entity("PaymentAPiInfrastructure.Entities.TransactionDetail", b =>
                {
                    b.HasOne("PaymentAPiInfrastructure.Entities.Transaction", null)
                        .WithMany("TransactionDetails")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PaymentAPiInfrastructure.Entities.Transaction", b =>
                {
                    b.Navigation("TransactionDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
