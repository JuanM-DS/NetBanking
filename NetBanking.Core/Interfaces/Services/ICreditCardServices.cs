using NetBanking.Core.CustomEntitys;
using NetBanking.Core.Entitys;
using NetBanking.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Interfaces.Services
{
	public interface ICreditCardServices
	{
		public Task AddAsync(CreditCard creditCard);

		public Task DeleteAsync(int creditCardId);

		public Task UpdateAsync(CreditCard creditCard);

		public Task<CreditCard?> GetByIdAsync(int creditCardId);

		public PagedList<CreditCard> GetAll(CreditCardQueryServices filters);
	}
}
