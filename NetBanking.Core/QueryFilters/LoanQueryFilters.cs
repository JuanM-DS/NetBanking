using NetBanking.Core.Entitys;
using NetBanking.Core.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.QueryFilters
{
	public class LoanQueryFilters
	{
		public int? UserId { get; set; }

		public DateOnly? StartDate { get; set; }

		public StatusType? LoanStatus { get; set; }
		public int PageSize { get; set; }
		public int CurrentPage { get; set; }
	}
}
