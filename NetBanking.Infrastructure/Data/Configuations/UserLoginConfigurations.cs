using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetBanking.Core.Entitys;
using NetBanking.Core.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Infrastructure.Data.Configuations
{
    public class UserLoginConfigurations : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {

            builder.ToTable("UsersLogin");

            builder.HasKey(x=>x.Id).HasName("PK__UsersLog__107D568CBC9DEBFA");

            builder.Property(x => x.Id);
            builder.Property(x => x.UserName).HasMaxLength(50);
            builder.Property(x => x.Password).HasMaxLength(250);
            builder.Property(x => x.FirstName).HasMaxLength(50);
            builder.Property(x => x.Role).HasConversion(
                x=>x.ToString(),
                x=>(RoleType)Enum.Parse(typeof(RoleType),x)
                );
        }
    }
}
