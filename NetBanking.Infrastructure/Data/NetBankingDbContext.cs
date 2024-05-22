using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NetBanking.Core.Entitys;
namespace NetBanking.Infrastructure.Data;

public partial class NetBankingDbContext : DbContext
{
    public NetBankingDbContext()
    {
    }

    public NetBankingDbContext(DbContextOptions<NetBankingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BankTransaction> BankTransactions { get; set; }

    public virtual DbSet<Check> Checks { get; set; }

    public virtual DbSet<CreditCard> CreditCards { get; set; }

    public virtual DbSet<CurrentAccount> CurrentAccounts { get; set; }

    public virtual DbSet<Loan> Loans { get; set; }

    public virtual DbSet<SavingAccount> SavingsAccounts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserLogin> UsersLogins { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
