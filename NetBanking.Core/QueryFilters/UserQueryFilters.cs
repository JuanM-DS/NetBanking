using NetBanking.Core.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.EntityFilters
{
    public class UserQueryFilters
    {
        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public int? BirthDateYear { get; set; }

        public StatusType? UserStatus { get; set; }

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }
    }
}
