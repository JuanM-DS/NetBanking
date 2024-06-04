using NetBanking.Core.CustomEntitys;
using NetBanking.Core.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetBanking.Core.Entitys;


public partial class User : BaseEntity
{
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly? BirthDate { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public DateOnly RegistrationDate { get; set; }

    public UserStatus UserStatus { get; set; }

    public virtual ICollection<BankTransaction> BankTransactionIssuerUsers { get; set; } = new List<BankTransaction>();

    public virtual ICollection<BankTransaction> BankTransactionReceiverUsers { get; set; } = new List<BankTransaction>();

    public virtual ICollection<CreditCard> CreditCards { get; set; } = new List<CreditCard>();

    public virtual ICollection<CurrentAccount> CurrentAccounts { get; set; } = new List<CurrentAccount>();

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();

    public virtual ICollection<SavingAccount> SavingsAccounts { get; set; } = new List<SavingAccount>();
}