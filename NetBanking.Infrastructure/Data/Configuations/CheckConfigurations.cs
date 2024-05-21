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
    public class CheckConfigurations : IEntityTypeConfiguration<Check>
    {
        public void Configure(EntityTypeBuilder<Check> builder)
        {
            builder.ToTable("Checks");

            builder.HasKey(x => x.Id).HasName("PK__Checks__B816D9F077ABDBED");
            builder.HasIndex(x => x.ChequeNumber, "UQ__Checks__D886A85BC1A2A720").IsUnique();

            builder.Property(x => x.Id).HasColumnName("ChequeId");
            builder.Property(x => x.AccountId);
            builder.Property(x => x.ChequeNumber).HasMaxLength(20);
            builder.Property(x => x.Amount).HasColumnType("decimal(15,2)");
            builder.Property(x => x.IssuedDate).HasColumnType("date");
            builder.Property(x => x.IssuerName).HasMaxLength(100);
            builder.Property(x => x.ReceiverName).HasMaxLength(100);
            builder.Property(x => x.CheckStatus).HasConversion(
                x => x.ToString(),
                x => (CheckStatus)Enum.Parse(typeof(CheckStatus),x)
                );

            builder.HasOne(x=>x.Account).WithMany(d=>d.Checks)
                .HasForeignKey(x=>x.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Checks__AccountI__4AB81AF0");
        }
    }
}
