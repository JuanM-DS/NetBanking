using Microsoft.Extensions.Options;
using NetBanking.Core.CustomEntitys;
using NetBanking.Core.Entitys;
using NetBanking.Core.Enumerables;
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
	public class CurrentAccountServices(IUnitOfWork unitOfWork, IOptions<PaginationOptions> paginationOptions) : ICurrentAccountServices
	{
		private readonly IUnitOfWork _unitOfWork = unitOfWork;
		private readonly PaginationOptions _paginationOptions = paginationOptions.Value;

		public async Task IssuerUserValidations(CurrentAccount currentAccount)
		{
			#region "User Validations"
			var issuerUser = await _unitOfWork.UserRepository.GetByIdAsync(currentAccount.UserId);
			if (issuerUser == null)
				throw new BusinessLogicException($"The User With id: {currentAccount.UserId} does not exists");

			if (issuerUser.UserStatus != UserStatus.active)
				throw new BusinessLogicException($"The User: {issuerUser.UserName} does not have a status required");
			#endregion
		}

		public async Task AddAsync(CurrentAccount currentAccount)
		{
			await IssuerUserValidations(currentAccount);

			try
			{
				await _unitOfWork.CurrentAccountRepository.AddAsync(currentAccount);
				await _unitOfWork.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new BusinessLogicException(ex.Message);
			}
		}

		public async Task DeleteAsync(int currentAccountId)
		{
			try
			{
				await _unitOfWork.CurrentAccountRepository.DeleteAsync(currentAccountId);
				await _unitOfWork.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new BusinessLogicException(ex.Message);
			}
		}

		public PagedList<CurrentAccount> GetAllAsync(CurrentAccountQueryFilters filters)
		{
			var currentAccounts = _unitOfWork.CurrentAccountRepository.GetAllWithEagerLoding(x=>x.User);

			filters.CurrentPage = (filters.CurrentPage == 0) ? _paginationOptions.CurrentPage : filters.CurrentPage;
			filters.PageSize = (filters.PageSize == 0) ? _paginationOptions.PageSize : filters.PageSize;

			if (filters.IdentifierNumber != null)
				currentAccounts = currentAccounts.Where(x => x.IdentifierNumber == filters.IdentifierNumber);

			if (filters.UserId != null)
				currentAccounts = currentAccounts.Where(x => x.UserId == filters.UserId);

			if (filters.ProductType != null)
				currentAccounts = currentAccounts.Where(x => x.ProductType == filters.ProductType);

			if (filters.ProductStatus != null)
				currentAccounts = currentAccounts.Where(x => x.ProductStatus == filters.ProductStatus);

			return PagedList<CurrentAccount>.Create(currentAccounts, filters.CurrentPage, filters.PageSize);
		}

		public async Task<CurrentAccount?> GetByIdAsync(int currentAccountId)
		{
			return await _unitOfWork.CurrentAccountRepository.GetByIdWithEagerLodingAsync(currentAccountId, x=>x.User);
		}

		public async Task UpdateAsync(CurrentAccount currentAccount)
		{
			await IssuerUserValidations(currentAccount);

			try
			{
				await _unitOfWork.CurrentAccountRepository.UpdateAsync(currentAccount);
				await _unitOfWork.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new BusinessLogicException(ex.Message);
			}
		}
	}
}
