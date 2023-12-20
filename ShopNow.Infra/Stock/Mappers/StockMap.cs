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
                .Property(x => x.Id)
                .HasColumnName("cd_stock");
            builder
                .Property(x => x.IdItem)
                .HasColumnName("id_item");
            builder
                .Property(x => x.Operation)
                .HasColumnName("operation");
            builder
                .Property(x => x.Count)
                .HasColumnName("count");
            builder
                .Property(x => x.Date)
                .HasColumnName("date");
            builder
                .HasOne(x => x.Item)
                .WithMany()
                .HasForeignKey(x => x.IdItem);
        }
    }
}
