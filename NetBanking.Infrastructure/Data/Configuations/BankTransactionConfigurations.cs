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
    public class BankTransactionConfigurations : IEntityTypeConfiguration<BankTransaction>
    {
        public void Configure(EntityTypeBuilder<BankTransaction> builder)
        {
            builder.ToTable("BankTransactions");

            builder.HasKey(x=>x.Id).HasName("PK__BankTran__55433A6BE7901D1A");

            builder.Property(x => x.TransactionDate).HasColumnType("datetime");
            builder.Property(x => x.TransactionType).HasConversion(
                x=>x.ToString(),
                x=>(TransactionType)Enum.Parse(typeof(TransactionType),x)
                );
            builder.Property(X => X.Amount).HasColumnType("decimal(15,2)");
            builder.Property(X => X.Description).HasMaxLength(255);
            builder.Property(X => X.IssuerUserId);
            builder.Property(X => X.ReceiverUserId);
            builder.Property(X => X.DestinationIdentifier).HasMaxLength(20);

            builder.HasOne(x=>x.IssuerUser).WithMany(d=>d.BankTransactionIssuerUsers)
                .HasForeignKey(x=>x.IssuerUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BankTrans__Issue__5165187F");

            builder.HasOne(x => x.ReceiverUser).WithMany(x => x.BankTransactionReceiverUsers)
                .HasForeignKey(x => x.ReceiverUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BankTrans__Recei__52593CB8");
        }
    }
}
