using FluentMigrator;

namespace ShopNow.Infra.Migrations
{
    [Migration(3)]
    public class AddTableOrder : Migration
    {
        public override void Up()
        {
            Create.Table("tb_order")
                .WithColumn("cd_order").AsInt32().PrimaryKey().Identity()
                .WithColumn("code").AsString(200).NotNullable()
                .WithColumn("cpf").AsString(11).NotNullable()
                .WithColumn("issue_date").AsDateTime().NotNullable()
                .WithColumn("freight").AsDecimal(18, 2).NotNullable()
                .WithColumn("sequence").AsInt32().NotNullable()
                .WithColumn("coupon_code").AsString(50).Nullable();

            Create.ForeignKey("FK_Order_Coupon")
                .FromTable("tb_order").ForeignColumn("coupon_code")
                .ToTable("tb_coupon").PrimaryColumn("code");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_Order_Coupon");
            Delete.Table("tb_order");
        }
    }
}
