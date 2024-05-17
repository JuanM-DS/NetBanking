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
    public class LoanConfigurations : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.ToTable("Loans");
            builder.HasKey(x=>x.Id).HasName("PK__Loans__4F5AD45783734C4E");

            builder.Property(x => x.Id).HasColumnName("LoanId");
            builder.Property(x => x.UserId);
            builder.Property(x => x.LoanAmount).HasColumnType("decimal(15,2)");
            builder.Property(x => x.InterestRate).HasColumnType("decimal(15,2)");
            builder.Property(x => x.LoanTermMonths);
            builder.Property(x => x.StartDate).HasColumnType("date");
            builder.Property(x => x.EndDate).HasColumnType("date");
            builder.Property(x => x.MonthlyPayment).HasColumnType("decimal(15,2)");
            builder.Property(x => x.LoanStatus).HasConversion(
                x=>x.ToString(),
                x=>(StatusType)Enum.Parse(typeof(StatusType),x)
                );

            builder.HasOne(x=>x.User).WithMany(d=>d.Loans)
                .HasForeignKey(x=>x.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Loans__UserId__4316F928");
        }
    }
}
