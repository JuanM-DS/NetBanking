using NetBanking.Core.Enumerables;
using System;
using System.Collections.Generic;

namespace NetBanking.Core.Entitys;


public partial class UserLogin : BaseEntity
{
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public RoleType Role { get; set; }
}
