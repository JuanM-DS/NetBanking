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
	public interface ISavingAccountServices
	{
		public Task AddAsync(SavingAccount savingAccount);
		public Task DeleteAsync(int savingAccountId);
		public Task UpdateAsync(SavingAccount savingAccount);
		public Task<SavingAccount?> GetByIdAsync(int savingaccountId);
		public PagedList<SavingAccount> GetAll(SavingAccountQueryFilters filters);
	}
}
