using NetBanking.Core.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Interfaces.Persistence
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }

        public IBaseRepository<SavingAccount> SavingAccountRepository { get;}

        public IBaseRepository<CurrentAccount> CurrentAccountRepository { get;}

        public IBaseRepository<Loan> LoanRepository { get; }

        public IBaseRepository<CreditCard> CreditCardRepository { get; }

        public IBaseRepository<Check> CheckRepository { get; }

        public IBaseRepository<Voucher> VoucherRepository { get; }

        public IBaseRepository<BankTransaction> BankTransactionRepository { get; }

        public IBaseRepository<UserLogin> UserLoginRepository { get;}


        public Task SaveChangesAsync();

        public void SaveChanges();
    }
}
