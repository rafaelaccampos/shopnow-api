using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopNow.Domain.Stock.Entities;

namespace ShopNow.Infra.Stock.Mappers
{
    public class StockMap : IEntityTypeConfiguration<StockEntry>
    {
        public void Configure(EntityTypeBuilder<StockEntry> builder)
        {
            builder
                .ToTable("tb_stock_entry");
            builder
                .HasKey(x => x.Id);
            builder
                .Property(x => x.Code)
                .HasColumnName("code");
        }
    }
}
