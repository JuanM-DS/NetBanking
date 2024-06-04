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
	public class SavingAccountServices(IUnitOfWork unitOfWork, IOptions<PaginationOptions> paginationOptions) : ISavingAccountServices
	{
		private readonly IUnitOfWork _unitOfWork = unitOfWork;
		private readonly PaginationOptions _paginationOptions = paginationOptions.Value;

		public async Task IssuerUserValidations(SavingAccount avingAccount)
		{
			var issuerUser = await _unitOfWork.UserRepository.GetByIdAsync(avingAccount.UserId);

			if (issuerUser == null)
				throw new BusinessLogicException($"the User with id: {avingAccount.UserId} does not exists");

			if (issuerUser.UserStatus != UserStatus.active)
				throw new BusinessLogicException($"The User: {issuerUser.UserName} does not have required status");
		}

		public async Task AddAsync(SavingAccount savingAccount)
		{
			await IssuerUserValidations(savingAccount);

			try
			{
				await _unitOfWork.SavingAccountRepository.AddAsync(savingAccount);
				await _unitOfWork.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new BusinessLogicException(ex.Message);
			}
		}

		public async Task DeleteAsync(int savingAccountId)
		{
			try
			{
				await _unitOfWork.SavingAccountRepository.DeleteAsync(savingAccountId);
				await _unitOfWork.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new BusinessLogicException(ex.Message);
			}
		}

		public PagedList<SavingAccount> GetAll(SavingAccountQueryFilters filters)
		{
			var savingAccounts = _unitOfWork.SavingAccountRepository.GetAllWithEagerLoding(x=>x.User);

			filters.CurrentPage = (filters.CurrentPage == 0) ? _paginationOptions.CurrentPage : filters.CurrentPage;
			filters.PageSize = (filters.PageSize == 0) ? _paginationOptions.PageSize : filters.PageSize;

			if (filters.UserId != null)
				savingAccounts = savingAccounts.Where(x => x.UserId == filters.UserId);

			if (filters.IdentifierNumber != null)
				savingAccounts = savingAccounts.Where(x => x.IdentifierNumber == filters.IdentifierNumber);

			if (filters.OpeningDate != null)
				savingAccounts = savingAccounts.Where(x => x.OpeningDate == filters.OpeningDate);

			if (filters.ProductStatus != null)
				savingAccounts = savingAccounts.Where(x => x.ProductStatus == filters.ProductStatus);

			return PagedList<SavingAccount>.Create(savingAccounts, filters.CurrentPage, filters.PageSize);
		}

		public async Task<SavingAccount?> GetByIdAsync(int savingAccountId)
		{
			return await _unitOfWork.SavingAccountRepository.GetByIdWithEagerLodingAsync(savingAccountId, x=>x.User);
		}

		public async Task UpdateAsync(SavingAccount savingAccount)
		{
			await IssuerUserValidations(savingAccount);

			try
			{
				await _unitOfWork.SavingAccountRepository.UpdateAsync(savingAccount);
				await _unitOfWork.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new BusinessLogicException(ex.Message);
			}
		}
	}
}
