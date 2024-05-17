using NetBanking.Core.Enumerables;
using System;
using System.Collections.Generic;

namespace NetBanking.Core.Entitys;


public partial class CreditCard : BaseEntity
{
    public int UserId { get; set; }

    public string CardNumber { get; set; } = null!;

    public string CardHolderName { get; set; } = null!;

    public DateOnly ExpiryDate { get; set; }

    public decimal CreditLimit { get; set; }

    public decimal AvailableBalance { get; set; }

    public StatusType CreditCardStatus { get; set; }

    public virtual User User { get; set; } = null!;
}
