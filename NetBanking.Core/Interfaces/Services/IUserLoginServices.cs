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
	public interface IUserLoginServices
	{
		public Task AddAsync(UserLogin userLogin);
		public Task UpdateAsync(UserLogin userLogin);
		public Task DeleteAsync(int userLoginId);
		public Task<UserLogin?> GetByIdAsync(int userLoginId);
		public PagedList<UserLogin> GetAll(UserLoginQueryFilters filters);

	}
}
