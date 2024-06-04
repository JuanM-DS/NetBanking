using NetBanking.Core.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Interfaces.Persistence
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        public Task AddAsync(T entity);

        public Task UpdateAsync(T entity);

        public Task DeleteAsync(int modelId);

        public IEnumerable<T> GetAll();

        public Task<T?> GetByIdAsync(int id);

        public IEnumerable<T> GetAllWithEagerLoding(params Expression<Func<T,object>>[] includes);

        public Task<T?> GetByIdWithEagerLodingAsync(int modelId, params Expression<Func<T, object>>[] includes);
    }
}
