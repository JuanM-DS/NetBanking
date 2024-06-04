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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Services
{
    public class CheckServices(IUnitOfWork unitOfWork, IOptions<PaginationOptions> paginationOptions) : ICheckServices
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly PaginationOptions _paginationOptions = paginationOptions.Value;

        public async Task AddAsync(Check model)
        {
            await CheckValidations(model);

            try
            {
                await _unitOfWork.CheckRepository.AddAsync(model);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new BusinessLogicException(ex.Message); ;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _unitOfWork.CheckRepository.DeleteAsync(id);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message); ;
            }
        }

        public IEnumerable<Check> GetAll(CheckQueryFilters filters)
        {
            var checks = _unitOfWork.CheckRepository.GetAllWithEagerLoding(x=>x.Account);

            #region Check Filters
            filters.PageSize = (filters.PageSize == 0) ? _paginationOptions.PageSize : filters.PageSize;
            filters.CurrentPage = (filters.CurrentPage == 0) ? _paginationOptions.CurrentPage : filters.CurrentPage;

            if (filters.AccountId != null)
                checks = checks.Where(a => a.AccountId == filters.AccountId);

            if (filters.CheckNumber != null)
                checks = checks.Where(a=>a.CheckNumber == filters.CheckNumber);

            if (filters.IssuedDate != null)
                checks = checks.Where(a => a.IssuedDate == filters.IssuedDate);

            if (filters.ReceiverName != null)
                checks = checks.Where(a => a.ReceiverName == filters.ReceiverName);

            if (filters.CheckStatus != null)
                checks = checks.Where(a => a.CheckStatus == filters.CheckStatus);
            #endregion
            var pagedList = PagedList<Check>.Create(checks,filters.CurrentPage, filters.PageSize);
            return pagedList;
        }

        public async Task<Check?> GetByIdAsync(int idModel)
        {
            return await _unitOfWork.CheckRepository.GetByIdWithEagerLodingAsync(idModel,x=>x.Account);
        }

        public async Task UpdateAsync(Check model)
        {
            #region Validations
            await CheckValidations(model);
            #endregion

            try
            {
                await _unitOfWork.CheckRepository.UpdateAsync(model);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message); ;
            }
        }

        private async Task CheckValidations(Check model)
        {
            #region Validations
            // account Id
            var currentAccount = await _unitOfWork.CurrentAccountRepository.GetByIdAsync(model.AccountId);

            if (currentAccount == null)
                throw new BusinessLogicException("The account doesnt exists");

			var issuerUser = await _unitOfWork.UserRepository.GetByIdAsync(currentAccount.UserId);

			if (issuerUser == null)
				throw new BusinessLogicException($"the User with id: {currentAccount.UserId} does not exists");

			if (issuerUser.UserStatus != UserStatus.active)
				throw new BusinessLogicException($"The User: {issuerUser.UserName} does not have required status");

			if (issuerUser.FirstName != model.IssuerName)
                throw new BusinessLogicException("The issuer name does not match the account owner");
            #endregion
        }
    }
}
