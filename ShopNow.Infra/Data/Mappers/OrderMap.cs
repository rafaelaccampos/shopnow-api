using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopNow.Domain.Entities;

namespace ShopNow.Infra.Data.Mappers
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .ToTable("tb_order");
            builder
                .HasKey(x => x.Id);
            builder
                .Property(x => x.Id)
                .HasColumnName("cd_order");
            builder
                .Property(x => x.Code)
                .HasColumnName("code");
            builder
                .Property(x => x.CpfNumber)
                .HasColumnName("cpf");
            builder.Ignore(x => x.Cpf);
            builder
                .Property(x => x.IssueDate)
                .HasColumnName("issue_date");
            builder
                .Property(x => x.Freight)
                .HasColumnName("freight")
                 .HasPrecision(18, 2);
            builder
                .Property(x => x.Sequence)
                .HasColumnName("sequence");
            builder
                .Property(x => x.IdCoupon)
                .HasColumnName("coupon_code");

            builder
                .HasOne(x => x.Coupon)
                .WithMany()
                .HasForeignKey(x => x.IdCoupon);
            builder.HasMany(x => x.OrderItems)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.IdOrder);
        }
    }
}
