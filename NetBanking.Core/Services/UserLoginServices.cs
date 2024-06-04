using Microsoft.Extensions.Options;
using NetBanking.Core.CustomEntitys;
using NetBanking.Core.Entitys;
using NetBanking.Core.Exceptions;
using NetBanking.Core.Interfaces.Persistence;
using NetBanking.Core.Interfaces.Services;
using NetBanking.Core.Options;
using NetBanking.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Services
{
	public class UserLoginServices(IUnitOfWork unitOfWork, IOptions<PaginationOptions> paginationOptions) : IUserLoginServices
	{
		private readonly IUnitOfWork _unitOfWork = unitOfWork;
		private readonly PaginationOptions _paginationOptions = paginationOptions.Value;

		public async Task AddAsync(UserLogin userLogin)
		{
			var IsUserLoginNameValid = _unitOfWork.UserLoginRepository.GetAll().Any(x=>x.UserName == userLogin.UserName);

			if (IsUserLoginNameValid)
				throw new BusinessLogicException($"The user name: {userLogin.UserName} already exists");

			try
			{
				await _unitOfWork.UserLoginRepository.AddAsync(userLogin);
				await _unitOfWork.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new BusinessLogicException(ex.Message);
			}
		}

		public async Task DeleteAsync(int userLoginId)
		{
			try
			{
				await _unitOfWork.UserLoginRepository.DeleteAsync(userLoginId);
				await _unitOfWork.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new BusinessLogicException(ex.Message);
			}
		}

		public PagedList<UserLogin> GetAll(UserLoginQueryFilters filters)
		{
			var userLogins = _unitOfWork.UserLoginRepository.GetAll();

			filters.CurrentPage = (filters.CurrentPage == 0) ? _paginationOptions.CurrentPage : filters.CurrentPage;
			filters.PageSize = (filters.PageSize == 0) ? _paginationOptions.PageSize : filters.PageSize;

			if (filters.UserName != null)
				userLogins = userLogins.Where(x => x.UserName == filters.UserName);

			if (filters.FirstName != null)
				userLogins = userLogins.Where(x => x.FirstName == filters.FirstName);

			if (filters.Role != null)
				userLogins = userLogins.Where(x => x.Role == filters.Role);

			return PagedList<UserLogin>.Create(userLogins, filters.CurrentPage, filters.PageSize);
		}

		public async Task<UserLogin?> GetByIdAsync(int userLoginId)
		{
			return await _unitOfWork.UserLoginRepository.GetByIdAsync(userLoginId);
		}

		public async Task UpdateAsync(UserLogin userLogin)
		{
			var IsUserLoginNameValid = _unitOfWork.UserLoginRepository.GetAll().Any(x => x.UserName == userLogin.UserName);

			if (IsUserLoginNameValid)
				throw new BusinessLogicException($"The user name: {userLogin.UserName} already exists");

			try
			{
				await _unitOfWork.UserLoginRepository.UpdateAsync(userLogin);
				await _unitOfWork.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new BusinessLogicException(ex.Message);
			}
		}
	}
}
