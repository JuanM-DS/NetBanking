using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetBanking.Core.Entitys;
using NetBanking.Core.Exceptions;
using NetBanking.Core.Interfaces.Persistence;
using NetBanking.Infrastructure.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Infrastructure.Persistence.Repository
{
    public class BaseRepository<T>(NetBankingDbContext context, IMapper mapper) : IBaseRepository<T> where T : BaseEntity
    {
        protected DbSet<T> _entity = context.Set<T>();
		private readonly IMapper _mapper = mapper;

		public async Task DeleteAsync(int modelId)
        {
            var entity = await _entity.FindAsync(modelId);
            if (entity == null)
                throw new PersistenceLogicExeption($"The Model with id:{modelId} doesnt exists");

            _entity.Remove(entity);
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

        public async Task UpdateAsync(T Model)
        {
            var entity = await _entity.FindAsync(Model.Id);

            if (entity == null)
				throw new PersistenceLogicExeption($"The Model with id:{Model.Id} doesnt exists");

            _mapper.Map(Model, entity);

			_entity.Update(entity);
        }

        public IEnumerable<T> GetAllWithEagerLoding(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _entity;

            foreach (var i in includes)
            {
                query = query.Include(i);
            }

            return query.AsEnumerable();
        }

        public async Task<T?> GetByIdWithEagerLodingAsync(int modelId, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _entity;

            foreach(var i in includes)
            {
                query = query.Include(i);
            }
            return await query.FirstOrDefaultAsync(x=>x.Id == modelId);
        }
    }
}
