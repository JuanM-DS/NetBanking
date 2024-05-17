using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetBanking.Core.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBanking.Infrastructure.Data.Configuations
{
    public class VoucherConfigurations : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.ToTable("Vouchers");

            builder.HasKey(x => x.Id).HasName("PK__Vouchers__3AEE7921ED8754BD");

            builder.Property(x => x.Id).HasColumnName("VoucherId");
            builder.Property(x => x.TransactionDate).HasColumnType("date");
            builder.Property(x => x.Amount).HasColumnType("decimal(15,2)");
            builder.Property(x => x.Description).HasMaxLength(255);
            builder.Property(x => x.IssuerUserId);
            builder.Property(x => x.ReceiverUserId);
            builder.Property(x => x.DestinationIdentifier).HasMaxLength(20);

            builder.HasOne(x=>x.IssuerUser).WithMany(d=>d.VoucherIssuerUsers) 
                   .HasForeignKey(x=>x.IssuerUserId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK__Vouchers__Issuer__4D94879B");

            builder.HasOne(x => x.ReceiverUser).WithMany(x => x.VoucherReceiverUsers)
                    .HasForeignKey(x => x.ReceiverUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Vouchers__Receiv__4E88ABD4");
        }
    }
}
