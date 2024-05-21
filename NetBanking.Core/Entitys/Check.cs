using NetBanking.Core.Enumerables;
using System;
using System.Collections.Generic;

namespace NetBanking.Core.Entitys;

public partial class Check : BaseEntity
{
    public int AccountId { get; set; }

    public string ChequeNumber { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateOnly IssuedDate { get; set; }

    public string IssuerName { get; set; } = null!;

    public string ReceiverName { get; set; } = null!;

    public CheckStatus CheckStatus { get; set; }

    public virtual CurrentAccount Account { get; set; } = null!;
}
