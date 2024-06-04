using NetBanking.Core.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.QueryFilters
{
	public class UserLoginQueryFilters
	{
		public string? UserName { get; set; } = null!;

		public string? FirstName { get; set; } = null!;

		public RoleType? Role { get; set; }

		public int CurrentPage { get; set; }

		public int PageSize { get; set; }
	}
}
