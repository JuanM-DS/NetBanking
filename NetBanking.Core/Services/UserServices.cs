using Microsoft.Extensions.Options;
using NetBanking.Core.CustomEntitys;
using NetBanking.Core.EntityFilters;
using NetBanking.Core.Entitys;
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
            validateUser(user);

            // insert
            await _unitOfWork.UserRepository.AddAsync(user);
            try
            {
                 await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ServicesExceptions(ex.Message);
            }
        }

        public async Task DeleteAsync(int idModel)
        {
            //validation
            var model = await _unitOfWork.UserRepository.GetByIdAsync(idModel);

            if (model == null)
                throw new ServicesExceptions($"The User with id:{idModel} not exists");

            // delete
            _unitOfWork.UserRepository.Delete(model);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ServicesExceptions(ex.Message);
            }
        }

        public PagedList<User> GetAll(UserQueryFilters filters)
        {
            var users = _unitOfWork.UserRepository.GetAll();

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

            if(filters.UserStatus != null)
                users = users.Where(x => x.UserStatus == filters.UserStatus);
            #endregion

            var pagedList = PagedList<User>.Create(users, filters.CurrentPage, filters.PageSize); 
            return pagedList;
        }

        public async Task<User?> GetByIdAsync(int idModel)
        {
            return await _unitOfWork.UserRepository.GetByIdAsync(idModel);
        }

        public async Task UpdateAsync(User user)
        {
            //validation
            var idModel = user.Id;

            var model = await _unitOfWork.UserRepository.GetByIdAsync(idModel);

            if (model == null)
                throw new ServicesExceptions($"The User with id:{idModel} not exists");

            validateUser(user);

            //update
            _unitOfWork.UserRepository.Update(user);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ServicesExceptions(ex.Message);
            }
        }

        private void validateUser(User user)
        {
            //business validations

            #region UserName
            var userName = user.UserName;

            var isUserNameValid = _unitOfWork.UserRepository.GetAll().Where(x => x.UserName == userName).Count() == 0;

            if (isUserNameValid)
                throw new ServicesExceptions($"The user name:{userName} already exists");
            #endregion

            #region UserEmail
            var userEmail = user.Email;

            var isUserEmailValid = _unitOfWork.UserRepository.GetAll().Where(x => x.Email == userEmail).Count() == 0;

            if (isUserNameValid)
                throw new ServicesExceptions($"The user email:{userEmail} already exists");
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
                throw new ServicesExceptions($"The user must to be older");
            #endregion

        }
    }
}
