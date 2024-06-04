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
	public class LoanServices(IUnitOfWork unitOfWork, IOptions<PaginationOptions> paginationOptions) : ILoanServices
	{
		private readonly IUnitOfWork _unitOfWork = unitOfWork;
		private readonly PaginationOptions paginationOptions = paginationOptions.Value;

		public async Task IssuerUserValidations(Loan loan)
		{
			var issuerUser = await _unitOfWork.UserRepository.GetByIdAsync(loan.UserId);

			if (issuerUser == null)
				throw new BusinessLogicException($"the User with id: {loan.UserId} does not exists");

			if (issuerUser.UserStatus != UserStatus.active)
				throw new BusinessLogicException($"The User: {issuerUser.UserName} does not have required status");
		}

		public async Task AddAsync(Loan loan)
		{
			await IssuerUserValidations(loan);

			try
			{
				await _unitOfWork.LoanRepository.AddAsync(loan);
				await _unitOfWork.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new BusinessLogicException(ex.Message);
			}
		}

		public PagedList<Loan> GetAll(LoanQueryFilters filters)
		{
			var loans = _unitOfWork.LoanRepository.GetAllWithEagerLoding(x=>x.User);

			filters.CurrentPage = (filters.CurrentPage == 0)? paginationOptions.CurrentPage : filters.CurrentPage;
			filters.PageSize = (filters.PageSize == 0) ? paginationOptions.PageSize : filters.PageSize;

			if (filters.UserId != null)
				loans = loans.Where(x => x.UserId == filters.UserId);

			if (filters.StartDate != null)
				loans = loans.Where(x => x.StartDate == filters.StartDate);

			if (filters.LoanStatus != null)
				loans = loans.Where(x => x.LoanStatus == filters.LoanStatus);

			return PagedList<Loan>.Create(loans, filters.CurrentPage, filters.PageSize);
		}

		public async Task<Loan?> GetByIdAsync(int loanId)
		{
			return await _unitOfWork.LoanRepository.GetByIdWithEagerLodingAsync(loanId, x=>x.User);
		}

		public async Task RemoveAsync(int loanId)
		{
			try
			{
				await _unitOfWork.LoanRepository.DeleteAsync(loanId);
				await _unitOfWork.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new BusinessLogicException(ex.Message);
			}
		}

		public async Task UpdateAsync(Loan loan)
		{
			await IssuerUserValidations(loan);

			try
			{
				await _unitOfWork.LoanRepository.UpdateAsync(loan);
				await _unitOfWork.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new BusinessLogicException(ex.Message);
			}
		}
	}
}
