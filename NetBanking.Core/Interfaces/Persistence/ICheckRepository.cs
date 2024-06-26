﻿using NetBanking.Core.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Interfaces.Persistence
{
    public interface ICheckRepository : IBaseRepository<Check>
    {
        public IEnumerable<Check> GetCirculatingMethod();
    }
}
