using Microsoft.EntityFrameworkCore;
using NetBanking.Core.Entitys;
using NetBanking.Core.Exceptions;
using NetBanking.Core.Interfaces.Persistence;
using NetBanking.Infrastructure.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Infrastructure.Persistence.Repository
{
    public class BaseRepository<T>(NetBankingDbContext context) : IBaseRepository<T> where T : BaseEntity
    {
        protected DbSet<T> _entity = context.Set<T>();

        public void Delete(T Model)
        {
            _entity.Remove(Model);
        }

        public IEnumerable<T> GetAll()
        {
            return _entity.AsEnumerable();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _entity.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _entity.AddAsync(entity);
        }

        public void Update(T Model)
        {
            _entity.Update(Model);
        }
    }
}
