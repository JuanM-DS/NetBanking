using NetBanking.Core.Enumerables;
using System;
using System.Collections.Generic;

namespace NetBanking.Core.Entitys;


public partial class BankTransaction : BaseEntity
{
    public DateTime TransactionDate { get; set; }

    public TransactionType TransactionType { get; set; }

    public decimal Amount { get; set; }

    public string? Description { get; set; }

    public int IssuerUserId { get; set; }

    public int ReceiverUserId { get; set; }

    public string? DestinationIdentifier { get; set; }

    public virtual User IssuerUser { get; set; } = null!;

    public virtual User ReceiverUser { get; set; } = null!;
}
