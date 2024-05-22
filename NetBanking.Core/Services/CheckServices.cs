using Microsoft.Extensions.Options;
using NetBanking.Core.Entitys;
using NetBanking.Core.Interfaces.Persistence;
using NetBanking.Core.Interfaces.Services;
using NetBanking.Core.Options;
using NetBanking.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Services
{
    public class CheckServices(IUnitOfWork unitOfWork, IOptions<PaginationOptions> paginationOptions) : ICheckServices
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly PaginationOptions _paginationOptions = paginationOptions.Value;

        public Task AddAsync(Check model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Check> GetAll(CheckQueryFilters filters)
        {
            throw new NotImplementedException();
        }

        public Task<Check> GetByIdAsync(int idModel)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Check model)
        {
            throw new NotImplementedException();
        }
    }
}
