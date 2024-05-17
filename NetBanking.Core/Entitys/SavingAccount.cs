using System;
using System.Collections.Generic;

namespace NetBanking.Core.Entitys;


public partial class SavingAccount : BaseProduct
{
    public decimal? InterestRate { get; set; }
}
