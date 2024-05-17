using NetBanking.Core.Entitys;
using NetBanking.Core.Exceptions;
using NetBanking.Core.Interfaces.Persistence;
using NetBanking.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Infrastructure.Persistence.Repository
{
    public class UserRepository : BaseRepository<User> , IUserRepository
    {
        public UserRepository(NetBankingDbContext context) : base(context)
        {
        }

        public async override Task AddAsync(User entity)
        {
            #region user name
            var username = entity.UserName;
            var isUserNameValid = (_entity.Where(x => x.UserName == username) != null) ? true : false;

            if (isUserNameValid)
                throw new PersistenceLogicException("The UserName is already in use");
            #endregion
            
            #region email
            var email = entity.Email;
            var isEmailValid = (_entity.Where(x => x.Email == email) != null) ? true : false;

            if (isEmailValid)
                throw new PersistenceLogicException("The Email is already in use");

            #endregion

            await base.AddAsync(entity);
        }
    }
}
