using NetBanking.Core.Entitys;
using NetBanking.Core.Exceptions;
using NetBanking.Core.Interfaces.Persistence;
using NetBanking.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Services
{
    public class UserServices(IUnitOfWork unitOfWork) : IUserServices
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

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
            await _unitOfWork.UserRepository.DeleteAsync(idModel);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ServicesExceptions(ex.Message);
            }
        }

        public IEnumerable<User> GetAll()
        {
            return _unitOfWork.UserRepository.GetAll();
        }

        public async Task<User> GetByIdAsync(int idModel)
        {
            return await _unitOfWork.UserRepository.GetByIdAsync(idModel);
        }

        public async Task UpdateAsync(User user)
        {
            validateUser(user);

            await _unitOfWork.UserRepository.UpdateAsync(user);

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
