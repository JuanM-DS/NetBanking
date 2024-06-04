using NetBanking.Core.Entitys;
using NetBanking.Core.Exceptions;
using NetBanking.Core.Interfaces.Persistence;
using NetBanking.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Services
{
    public class ProductServices(IUnitOfWork unitOfWork) : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<IBaseProduct>> GetProductsByUserId(int userId)
        {
            var user = await _unitOfWork.UserRepository.GetByIdWithEagerLodingAsync(
                userId, 
                x => x.BankTransactionIssuerUsers, 
                x => x.BankTransactionReceiverUsers, 
                x => x.CreditCards, 
                x => x.CurrentAccounts, 
                x => x.Loans, 
                x => x.SavingsAccounts
                );

            if (user == null)
                throw new BusinessLogicException($"The User with Id:{userId} does not exists");

            var creditsCards = user.CreditCards;
            var savingAccount = user.SavingsAccounts;
            var currentAccount = user.CurrentAccounts;


            return new List<IBaseProduct>().Concat(creditsCards).Concat(savingAccount).Concat(currentAccount);
        }
    }
}
