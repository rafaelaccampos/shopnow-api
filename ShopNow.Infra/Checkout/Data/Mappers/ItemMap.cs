using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopNow.Domain.Checkout.Entities;

namespace ShopNow.Infra.Checkout.Data.Mappers
{
    public class ItemMap : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder
                .ToTable("tb_item");
            builder
                .HasKey(x => x.Id);
            builder
                .Property(x => x.Id)
                .HasColumnName("cd_item");
            builder
                .Property(x => x.Description)
                .HasColumnName("description");
            builder
                .Property(x => x.Category)
                .HasColumnName("category");
            builder
                .Property(x => x.Price)
                .HasColumnName("price")
                .HasPrecision(18, 2); ;
            builder
                .Property(x => x.Width)
                .HasColumnName("width")
                .HasPrecision(18, 2);
            builder
                .Property(x => x.Height)
                .HasColumnName("height")
                .HasPrecision(18, 2);
            builder
                .Property(x => x.Length)
                .HasColumnName("length")
                .HasPrecision(18, 2);
            builder
                .Property(x => x.Weight)
                .HasColumnName("weight")
                .HasPrecision(18, 2);
        }
    }
}
