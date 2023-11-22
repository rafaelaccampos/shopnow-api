using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopNow.Domain.Checkout.Entities;

namespace ShopNow.Infra.Checkout.Data.Mappers
{
    public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder
                .ToTable("tb_order_items");
            builder
                .Property(x => x.IdOrder)
                .HasColumnName("cd_order");
            builder
                .HasKey(x => new { x.IdItem, x.IdOrder });
            builder
                .Property(x => x.IdItem)
                .HasColumnName("cd_item");
            builder
                .Property(x => x.Price)
                .HasColumnName("price")
                .HasPrecision(18, 2);
            builder
                .Property(x => x.Count)
                .HasColumnName("count");

            builder
                .HasOne(x => x.Item)
                .WithMany()
                .HasForeignKey(x => x.IdItem);
            builder.HasOne(x => x.Order)
                .WithMany(x => x.OrderItems)
                .HasForeignKey(x => x.IdOrder);
        }
    }
}
