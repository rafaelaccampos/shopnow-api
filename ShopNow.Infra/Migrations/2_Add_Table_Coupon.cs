using FluentMigrator;

namespace ShopNow.Infra.Migrations
{
    [Migration(2)]
    public class AddTableCoupon : Migration
    {
        public override void Up()
        {
            Create.Table("tb_coupon")
                .WithColumn("code").AsString(50).NotNullable().PrimaryKey()
                .WithColumn("percentual").AsDecimal(18, 2).NotNullable()
                .WithColumn("expired_date").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Table("tb_coupon");
        }
    }
}
