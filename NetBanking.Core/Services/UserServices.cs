using Microsoft.Extensions.Options;
using NetBanking.Core.CustomEntitys;
using NetBanking.Core.EntityFilters;
using NetBanking.Core.Entitys;
using NetBanking.Core.Enumerables;
using NetBanking.Core.Exceptions;
using NetBanking.Core.Interfaces.Persistence;
using NetBanking.Core.Interfaces.Services;
using NetBanking.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Services
{
    public class UserServices(IUnitOfWork unitOfWork, IOptions<PaginationOptions> paginationOptions) : IUserServices
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly PaginationOptions _paginationOptions = paginationOptions.Value;
        private const int minimumAge = 18;

        public async Task AddAsync(User user)
        {
            ValidateUser(user);

            // insert
            try
            {
                await _unitOfWork.UserRepository.AddAsync(user);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        public async Task DeleteAsync(int modelId)
        {     
			try
            {
                await _unitOfWork.UserRepository.DeleteAsync(modelId);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        public PagedList<User> GetAll(UserQueryFilters filters)
        {
            var users = _unitOfWork.UserRepository.GetAllWithEagerLoding(
                x => x.BankTransactionIssuerUsers,
                x => x.BankTransactionReceiverUsers,
                x => x.CreditCards,
                x => x.CurrentAccounts,
                x => x.Loans,
                x => x.SavingsAccounts
                );

            filters.CurrentPage = (filters.CurrentPage == 0) ? _paginationOptions.CurrentPage : filters.CurrentPage;
            filters.PageSize = (filters.PageSize == 0) ? _paginationOptions.PageSize : filters.PageSize;

            #region User Filters
            if (filters.UserName != null)
                users = users.Where(x => x.UserName == filters.UserName);

            if(filters.FirstName != null)
                users = users.Where(x => x.FirstName == filters.FirstName);

            if (filters.LastName != null)
                users = users.Where(x => x.LastName == filters.LastName);

            if(filters.Email != null)
                users = users.Where(x => x.Email == filters.Email);

            if(filters.BirthDateYear != null)
                users = users.Where(x => Convert.ToDateTime(x.BirthDate).Year == filters.BirthDateYear);


            #endregion

            var pagedList = PagedList<User>.Create(users, filters.CurrentPage, filters.PageSize); 
            return pagedList;
        }

        public async Task<User?> GetByIdAsync(int modelId)
        {
            return await _unitOfWork.UserRepository.GetByIdWithEagerLodingAsync(
                modelId,
                x => x.BankTransactionIssuerUsers,
                x => x.BankTransactionReceiverUsers,
                x => x.CreditCards,
                x => x.CurrentAccounts,
                x => x.Loans,
                x => x.SavingsAccounts
                );
        }

        public async Task UpdateAsync(User user)
        {
			//validations
			var model = await _unitOfWork.UserRepository.GetByIdAsync(user.Id);
			if (model == null)
				throw new BusinessLogicException($"The User with id:{user.Id} doesnt exists");

			ValidateUser(user);

            model.RegistrationDate = user.RegistrationDate;

            //update
            try
            {
                await _unitOfWork.UserRepository.UpdateAsync(model);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        private void ValidateUser(User user)
        {
            //business validations

            #region UserName
            var userName = user.UserName;

            var isUserNameValid = !_unitOfWork.UserRepository.GetAll().Any(x => x.UserName == userName);

            if (isUserNameValid)
                throw new BusinessLogicException($"The user name:{userName} already exists");
            #endregion

            #region UserEmail
            var userEmail = user.Email;

            var isUserEmailValid =!_unitOfWork.UserRepository.GetAll().Any(x => x.Email == userEmail);

            if (isUserNameValid)
                throw new BusinessLogicException($"The user email:{userEmail} already exists");
            #endregion

            #region UserBirthDay
            var DateNow = DateTime.Now;
            var UserBirthDay = Convert.ToDateTime(user.BirthDate);
            var userAge = DateNow.Year - UserBirthDay.Year;

            if (DateNow.Month < UserBirthDay.Month ||
            (DateNow.Month == UserBirthDay.Month && DateNow.Day < UserBirthDay.Day))
            {
                userAge--;
            }

            var isUserBithDayValid = (userAge >= minimumAge);

            if (isUserBithDayValid)
                throw new BusinessLogicException($"The user must to be older");
            #endregion
        }
    }
}
