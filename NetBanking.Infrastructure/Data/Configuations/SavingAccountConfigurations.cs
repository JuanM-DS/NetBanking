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
    public class SavingAccountConfigurations : IEntityTypeConfiguration<SavingAccount>
    {
        public void Configure(EntityTypeBuilder<SavingAccount> entity)
        {
            entity.ToTable("SavingsAccounts");
            entity.HasKey(e => e.Id).HasName("PK__SavingsA__349DA5A640E06088");

            entity.HasIndex(e => e.IdentifierNumber, "UQ__SavingsA__BE2ACD6FD8C51357").IsUnique();

            entity.Property(e => e.IdentifierNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("AccountNumber");
            entity.Property(e => e.ProductStatus).HasColumnName("AccountStatus")
                  .HasConversion(
                    x=>x.ToString(),
                    x=>(StatusType)Enum.Parse(typeof(StatusType), x)
                    );
            entity.Property(x => x.Id).HasColumnName("AccountId");

            entity.Property(e => e.Balance).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.DailyWithdrawalLimit).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.InterestRate).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.LastTransactionDate).HasColumnType("datetime");
            entity.Property(e => e.OpeningDate).HasColumnType("date");
            entity.Property(x => x.ProducType).HasConversion(
                x => x.ToString(),
                x => (ProductType)Enum.Parse(typeof(ProductType), x)
                );

            entity.HasOne(d => d.User).WithMany(p => p.SavingsAccounts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SavingsAc__UserI__3C69FB99");
        }
    }
}
