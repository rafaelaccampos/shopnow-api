using FluentMigrator;

namespace ShopNow.Infra.Migrations
{
    [Migration(4)]
    public class AddTableOrderItem : Migration
    {
        public override void Up()
        {
            Create.Table("tb_order_items")
                .WithColumn("cd_order").AsInt32().NotNullable()
                .WithColumn("cd_item").AsInt32().NotNullable()
                .WithColumn("price").AsDecimal(18, 2).NotNullable()
                .WithColumn("count").AsInt32().NotNullable();

            Create.PrimaryKey("PK_Order_Items")
                .OnTable("tb_order_items")
                .Columns("cd_order", "cd_item");

            Create.ForeignKey("FK_OrderItems_Order")
                .FromTable("tb_order_items").ForeignColumn("cd_order")
                .ToTable("tb_order").PrimaryColumn("cd_order");

            Create.ForeignKey("FK_OrderItems_Item")
                .FromTable("tb_order_items").ForeignColumn("cd_item")
                .ToTable("tb_item").PrimaryColumn("cd_item");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_OrderItems_Order");
            Delete.ForeignKey("FK_OrderItems_Item");
            Delete.Table("tb_order_items");
        }
    }
}
