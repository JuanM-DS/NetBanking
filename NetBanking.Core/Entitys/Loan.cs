using NetBanking.Core.Enumerables;
using System;
using System.Collections.Generic;

namespace NetBanking.Core.Entitys;


public partial class Loan : BaseEntity
{
    public int UserId { get; set; }

    public decimal LoanAmount { get; set; }

    public decimal InterestRate { get; set; }

    public int LoanTermMonths { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public decimal? MonthlyPayment { get; set; }

    public StatusType LoanStatus { get; set; }

    public virtual User User { get; set; } = null!;
}
