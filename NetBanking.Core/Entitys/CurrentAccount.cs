using System;
using System.Collections.Generic;

namespace NetBanking.Core.Entitys;


public partial class CurrentAccount : BaseProduct
{
    public virtual ICollection<Check> Checks { get; set; } = new List<Check>();
}
