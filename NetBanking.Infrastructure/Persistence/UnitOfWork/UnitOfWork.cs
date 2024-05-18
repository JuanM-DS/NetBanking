﻿using NetBanking.Core.Entitys;
using NetBanking.Core.Interfaces.Persistence;
using NetBanking.Infrastructure.Data;
using NetBanking.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork(NetBankingDbContext context) : IUnitOfWork
    {
        #region Properties
        public IUserRepository UserRepository { get; } = new UserRepository(context);
        public IBaseRepository<SavingAccount> SavingAccountRepository { get; } = new BaseRepository<SavingAccount>(context);
        public IBaseRepository<CurrentAccount> CurrentAccountRepository { get; } = new BaseRepository<CurrentAccount>(context);
        public IBaseRepository<Loan> LoanRepository { get; } = new BaseRepository<Loan>(context);
        public IBaseRepository<CreditCard> CreditCardRepository { get; } = new BaseRepository<CreditCard>(context);
        public IBaseRepository<Check> CheckRepository { get; } = new BaseRepository<Check>(context);
        public IBaseRepository<Voucher> VoucherRepository { get; } = new BaseRepository<Voucher>(context);
        public IBaseRepository<BankTransaction> BankTransactionRepository { get; } = new BaseRepository<BankTransaction>(context);
        public IBaseRepository<UserLogin> UserLoginRepository { get; } = new BaseRepository<UserLogin>(context);
        #endregion

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}