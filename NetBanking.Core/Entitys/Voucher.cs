using System;
using System.Collections.Generic;

namespace NetBanking.Core.Entitys;

public partial class Voucher : BaseEntity
{
    public DateOnly TransactionDate { get; set; }

    public decimal Amount { get; set; }

    public string? Description { get; set; }

    public int IssuerUserId { get; set; }

    public int ReceiverUserId { get; set; }

    public string? DestinationIdentifier { get; set; }

    public virtual User IssuerUser { get; set; } = null!;

    public virtual User ReceiverUser { get; set; } = null!;
}
