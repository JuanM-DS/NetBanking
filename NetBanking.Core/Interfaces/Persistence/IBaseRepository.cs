using NetBanking.Core.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Interfaces.Persistence
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        public Task AddAsync(T entity);

        public void Update(T entity);

        public void Delete(T model);

        public IEnumerable<T> GetAll();

        public Task<T?> GetByIdAsync(int id);
    }
}
