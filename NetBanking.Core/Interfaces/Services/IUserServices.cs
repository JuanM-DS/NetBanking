using NetBanking.Core.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Interfaces.Services
{
    public interface IUserServices
    {
        public Task AddAsync(User user);

        public Task UpdateAsync(User user);

        public Task DeleteAsync(int idModel);

        public Task<User> GetByIdAsync(int idModel);

        public IEnumerable<User> GetAll();
    }
}
