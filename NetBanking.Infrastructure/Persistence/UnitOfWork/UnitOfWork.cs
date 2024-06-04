using AutoMapper;
using NetBanking.Core.DTOs;
using NetBanking.Core.Entitys;
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
    public class UnitOfWork(NetBankingDbContext context, IMapper mapper) : IUnitOfWork
    {
        #region Properties
        public IBaseRepository<User> UserRepository { get; } = new BaseRepository<User>(context, mapper);
        public IBaseRepository<SavingAccount> SavingAccountRepository { get; } = new BaseRepository<SavingAccount>(context, mapper);
        public IBaseRepository<CurrentAccount> CurrentAccountRepository { get; } = new BaseRepository<CurrentAccount>(context, mapper);
        public IBaseRepository<Loan> LoanRepository { get; } = new BaseRepository<Loan>(context, mapper);
        public IBaseRepository<CreditCard> CreditCardRepository { get; } = new BaseRepository<CreditCard>(context, mapper);
        public ICheckRepository CheckRepository { get; } = new CheckRepository(context, mapper);
        public IBaseRepository<VoucherDTO> VoucherRepository { get; } = new BaseRepository<VoucherDTO>(context, mapper);
        public IBaseRepository<BankTransaction> BankTransactionRepository { get; } = new BaseRepository<BankTransaction>(context, mapper);
        public IBaseRepository<UserLogin> UserLoginRepository { get; } = new BaseRepository<UserLogin>(context, mapper);
        #endregion

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
