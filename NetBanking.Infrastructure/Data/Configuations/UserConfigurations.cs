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
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("Users");
            entity.HasKey(e => e.Id).HasName("PK__Users__1788CC4C7C7559D2");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4EE02EB06").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534DFF426E4").IsUnique();

            entity.Property(e => e.AccountStatus).HasColumnName("AccountStatus")
                .HasConversion(
                    x => x.ToString(),
                    x => (StatusType)Enum.Parse(typeof(StatusType),x)
                );
            entity.Property(x => x.Id).HasColumnName("UserId");

            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.RegistrationDate).HasColumnType("date");
        }
    }
}
