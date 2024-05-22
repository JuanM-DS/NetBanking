using NetBanking.Core.Entitys;
using NetBanking.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Services
{
    public class ProductServices : IProductServices
    {
        public IEnumerable<IBaseProduct> GetProductsByUser(User user)
        {
            var creditsCards = user.CreditCards;
            var savingAccount = user.SavingsAccounts;
            var currentAccount = user.CurrentAccounts;


            return new List<IBaseProduct>().Concat(creditsCards).Concat(savingAccount).Concat(currentAccount);
        }
    }
}
