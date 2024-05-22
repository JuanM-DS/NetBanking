using NetBanking.Core.Enumerables;
using System;
using System.Collections.Generic;

namespace NetBanking.Core.Entitys;


public partial class CreditCard : BaseEntity, IBaseProduct
{
    public DateOnly ExpiryDate { get; set; }
    public decimal CreditLimit { get; set; }

    public string IdentifierNumber { get; set; } = null!;
    public int UserId { get; set; }
    public decimal Balance { get; set; }
    public DateOnly OpeningDate { get; set; }
    public StatusType ProductStatus { get; set; }
    public DateTime? LastTransactionDate { get; set; }
    public decimal DailyWithdrawalLimit { get; set; }
    public ProductType ProducType { get; set; }
    public virtual User? User { get; set; }
}
