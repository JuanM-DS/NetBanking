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
    public class CreditCardConfigurations : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder.ToTable("CreditCards");

            builder.HasIndex(x=>x.IdentifierNumber, "UQ__CreditCa__A4E9FFE91010947C").IsUnique();

            builder.Property(x => x.Id).HasColumnName("CardId");
            builder.Property(x => x.UserId);
            builder.Property(x=>x.IdentifierNumber).HasMaxLength(20).HasColumnName("CardNumber");
            builder.Property(x => x.ExpiryDate).HasColumnType("date");
            builder.Property(x => x.CreditLimit).HasColumnType("decimal(15,2)");
            builder.Property(x => x.Balance).HasColumnType("decimal(15,2)").HasColumnName("AvailableBalance");
            builder.Property(x => x.ProductStatus).HasColumnName("CreditCardStatus").HasConversion(
                x=>x.ToString(),
                x=>(StatusType)Enum.Parse(typeof(StatusType),x)
                );
            builder.Property(x => x.ProductType).HasConversion(
                x => x.ToString(),
                x => (ProductType)Enum.Parse(typeof(ProductType), x)
                );
            builder.Property(x => x.DailyWithdrawalLimit).HasColumnType("decimal(15,2)").HasColumnName("DailyWithdrawalLimit").IsRequired();
            builder.Property(x => x.LastTransactionDate).HasColumnType("datetime");
            builder.Property(x => x.OpeningDate).HasColumnType("date");

            builder.HasOne(x=>x.User).WithMany(d=>d.CreditCards)
                .HasForeignKey(x=>x.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CreditCar__UserI__46E78A0C");
        }
    }
}
