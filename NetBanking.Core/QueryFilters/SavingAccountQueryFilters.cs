using NetBanking.Core.Entitys;
using NetBanking.Core.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.QueryFilters
{
	public class SavingAccountQueryFilters
	{
		public string? IdentifierNumber { get; set; } = null!;
		public int? UserId { get; set; }
		public DateOnly? OpeningDate { get; set; }
		public StatusType? ProductStatus { get; set; }
		public ProductType? ProductType { get; set; }
		public int PageSize { get; set; }
		public int CurrentPage { get; set; }
	}
}
