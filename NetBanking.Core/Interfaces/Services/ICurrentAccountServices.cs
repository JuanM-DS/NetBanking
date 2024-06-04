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
	public interface ICurrentAccountServices
	{
		public Task AddAsync(CurrentAccount currentAccount);

		public Task DeleteAsync(int currentAccountId);

		public Task UpdateAsync(CurrentAccount currentAccount);

		public Task<CurrentAccount?> GetByIdAsync(int currentAccountId);

		public PagedList<CurrentAccount> GetAllAsync(CurrentAccountQueryFilters filters);
	}
}
