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

            await _unitOfWork.CheckRepository.AddAsync(model);
        }

        public async Task DeleteAsync(int id)
        {
            #region Validations
            var check = await _unitOfWork.CheckRepository.GetByIdAsync(id);

            if (check == null)
                throw new ServicesExceptions($"The Check with id:{id} doesnt exists");
            #endregion

            _unitOfWork.CheckRepository.Delete(check);
        }

        public IEnumerable<Check> GetAll(CheckQueryFilters filters)
        {
            var checks = _unitOfWork.CheckRepository.GetAll();

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
            return await _unitOfWork.CheckRepository.GetByIdAsync(idModel);
        }

        public async Task UpdateAsync(Check model)
        {
            #region Validations
            var check = await _unitOfWork.CheckRepository.GetByIdAsync(model.Id);

            if (check == null)
                throw new ServicesExceptions($"The Check with id:{model.Id} doesnt exists");

            await CheckValidations(model);
            #endregion

            check.Amount = model.Amount;
            check.ReceiverName = model.ReceiverName;

            _unitOfWork.CheckRepository.Update(check);
        }

        private async Task CheckValidations(Check model)
        {
            #region Validations
            // account Id
            var currentAccount = await _unitOfWork.CurrentAccountRepository.GetByIdAsync(model.Id);

            if (currentAccount == null)
                throw new ServicesExceptions("The account doesnt exists");

            // Account User Name
            var accountUsername = currentAccount.User.FirstName;

            if (accountUsername != model.IssuerName)
                throw new ServicesExceptions("The issuer name does not match the account owner");
            #endregion
        }
    }
}
