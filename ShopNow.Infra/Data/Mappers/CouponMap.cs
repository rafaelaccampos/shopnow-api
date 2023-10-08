using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopNow.Domain.Entities;

namespace ShopNow.Infra.Data.Mappers
{
    public class CouponMap : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder
                .ToTable("tb_coupon");
            builder
                .HasKey(x => x.Code);
            builder
                .Property(x => x.Code)
                .HasColumnName("code");
            builder
                .Property(x => x.Percentual)
                .HasColumnName("percentual");
            builder
                .Property(x => x.ExpiredDate)
                .HasColumnName("expired_date");
            builder
                .Ignore(x => x.ActualDate);
            builder
                .Ignore(x => x.Discount);
        }
    }
}
