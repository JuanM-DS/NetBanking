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
	public interface ILoanServices
	{
		public Task AddAsync(Loan loan);
		public Task RemoveAsync(int loanId);
		public Task UpdateAsync(Loan loan);
		public Task<Loan?> GetByIdAsync(int loanId);
		public PagedList<Loan> GetAll(LoanQueryFilters filters);
	}
}
