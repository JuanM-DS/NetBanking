using NetBanking.Core.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Core.Entitys
{
    public class BaseProduct : BaseEntity
    {
        public string IdentifierNumber { get; set; } = null!;

        public int UserId { get; set; }

        public decimal Balance { get; set; }

        public DateOnly OpeningDate { get; set; }

        public StatusType ProductStatus { get; set; }

        public DateTime? LastTransactionDate { get; set; }

        public decimal DailyWithdrawalLimit { get; set; }

        public ProductType ProducType { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
