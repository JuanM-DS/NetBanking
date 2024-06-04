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
    public class CurrentAccountConfigurations : IEntityTypeConfiguration<CurrentAccount>
    {
        public void Configure(EntityTypeBuilder<CurrentAccount> builder)
        {
            builder.ToTable("CurrentAccounts");
            builder.HasIndex(x => x.IdentifierNumber, "UQ__CurrentA__BE2ACD6F21F1E4F8").IsUnique();

            builder.Property(x => x.Id).HasColumnName("AccountId");
            builder.Property(x=>x.IdentifierNumber).HasMaxLength(20).HasColumnName("AccountNumber");
            builder.Property(x => x.UserId);
            builder.Property(x=>x.Balance).HasColumnType("decimal(15,2)");
            builder.Property(x => x.OpeningDate).HasColumnType("date");
            builder.Property(x => x.ProductStatus).HasColumnName("UserStatus").HasConversion(
                x=>x.ToString(),
                x=>(StatusType)Enum.Parse(typeof(StatusType),x)
                );
            builder.Property(x => x.DailyWithdrawalLimit).HasColumnType("decimal(15,2)").HasColumnName("DailyWithdrawalLimit").IsRequired();
            builder.Property(x => x.LastTransactionDate).HasColumnType("datetime");
            builder.Property(x => x.ProductType).HasConversion(
                x => x.ToString(),
                x => (ProductType)Enum.Parse(typeof(ProductType), x)
                );

            builder.HasOne(x=>x.User).WithMany(d=>d.CurrentAccounts)
                .HasForeignKey(x=>x.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CurrentAc__UserI__403A8C7D");
        }
    }
}
