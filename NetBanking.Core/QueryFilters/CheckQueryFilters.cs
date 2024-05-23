using NetBanking.Core.Entitys;
using NetBanking.Core.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.QueryFilters
{
    public class CheckQueryFilters
    {
        public int? AccountId { get; set; }

        public string? CheckNumber { get; set; }

        public DateOnly? IssuedDate { get; set; }

        public string? IssuerName { get; set; }

        public string? ReceiverName { get; set; }

        public CheckStatus? CheckStatus { get; set; }

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }
    }
}
