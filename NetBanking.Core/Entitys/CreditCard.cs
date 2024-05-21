using NetBanking.Core.Enumerables;
using System;
using System.Collections.Generic;

namespace NetBanking.Core.Entitys;


public partial class CreditCard : BaseProduct
{
    public DateOnly ExpiryDate { get; set; }

    public decimal CreditLimit { get; set; }
}
