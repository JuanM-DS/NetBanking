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

            builder.HasKey(x => x.Id).HasName("PK__CreditCa__55FECDAEB019CA6B");

            builder.HasIndex(x=>x.CardNumber, "UQ__CreditCa__A4E9FFE91010947C").IsUnique();

            builder.Property(x => x.Id).HasColumnName("CardId");
            builder.Property(x => x.UserId);
            builder.Property(x=>x.CardNumber).HasMaxLength(20);
            builder.Property(x => x.CardHolderName).HasMaxLength(100);
            builder.Property(x => x.ExpiryDate).HasColumnType("date");
            builder.Property(x => x.CreditLimit).HasColumnType("decimal(15,2)");
            builder.Property(x => x.AvailableBalance).HasColumnType("decimal(15,2)");
            builder.Property(x => x.CreditCardStatus).HasConversion(
                x=>x.ToString(),
                x=>(StatusType)Enum.Parse(typeof(StatusType),x)
                );
            builder.HasOne(x=>x.User).WithMany(x=>x.CreditCards)
                .HasForeignKey(x=>x.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CreditCar__UserI__46E78A0C");
        }
    }
}
