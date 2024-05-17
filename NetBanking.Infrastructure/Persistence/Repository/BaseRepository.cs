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

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            if(entity == null)
                throw new PersistenceLogicException($"Entity with id: {id}, not exists");

            _entity.Remove(entity);
        }

        public IEnumerable<T> GetAllAsync()
        {
            return _entity.AsEnumerable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _entity.FindAsync(id);
            if (entity == null)
                throw new PersistenceLogicException($"Entity with id: {id}, not found");

            return entity;
        }

        public async virtual Task AddAsync(T entity)
        {
            await _entity.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            var id = entity.Id;
            var exists = ((await _entity.FindAsync(id)) == null)? false : true;

            if (exists)
                throw new PersistenceLogicException($"Entity with id:{id}, not exists");

            _entity.Update(entity);
        }
    }
}
