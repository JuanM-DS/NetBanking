using NetBanking.Core.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Interfaces.Services
{
    public interface IProductServices
    {
        public Task<IEnumerable<IBaseProduct>> GetProductsByUserId(int userId);
    }
}
