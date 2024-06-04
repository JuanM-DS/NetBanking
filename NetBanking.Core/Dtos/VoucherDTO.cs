using System;
using System.Collections.Generic;
using NetBanking.Core.Entitys;

namespace NetBanking.Core.DTOs;

public partial class VoucherDTO : BaseEntity
{
    public DateOnly TransactionDate { get; set; }

    public decimal Amount { get; set; }

    public string? Description { get; set; }

    public int IssuerUserId { get; set; }

    public int ReceiverUserId { get; set; }

    public string? DestinationIdentifier { get; set; }
}
