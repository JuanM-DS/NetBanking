using NetBanking.Core.Entitys;
using NetBanking.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Interfaces.Services
{
    public interface ICheckServices
    {
        public Task<Check> GetByIdAsync(int idModel);

        public IEnumerable<Check> GetAll(CheckQueryFilters filters);

        public Task UpdateAsync(Check model);

        public Task DeleteAsync(int id);

        public Task AddAsync(Check model);
    }
}
