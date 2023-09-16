using FluentMigrator;

namespace ShopNow.Infra.Migrations
{
    [Migration(3)]
    public class AddTableOrderItem : Migration
    {
        public override void Up()
        {
            Create.Table("tb_order_items")
                .WithColumn("cd_order").AsInt32().PrimaryKey().Identity()
                .WithColumn("id_tem").AsInt32().NotNullable()
                .WithColumn("price").AsDecimal(18, 2).NotNullable()
                .WithColumn("count").AsInt32().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("tb_order_items");
        }
    }
}
