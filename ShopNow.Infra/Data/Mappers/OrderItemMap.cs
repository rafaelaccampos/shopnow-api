using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopNow.Domain.Entities;

namespace ShopNow.Infra.Data.Mappers
{
    public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder
                .ToTable("tb_order_item");
            builder
                .HasKey(x => x.Id);
            builder
                .Property(x => x.Id)
                .HasColumnName("cd_order_item");
            builder
                .Property(x => x.IdItem)
                .HasColumnName("cd_item");
            builder
                .Property(x => x.Price)
                .HasColumnName("price");
            builder
                .Property(x => x.Count)
                .HasColumnName("count");

            builder
                .HasOne(x => x.Item)
                .WithMany()
                .HasForeignKey(x => x.IdItem);
        }
    }
}
