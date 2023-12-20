using FluentMigrator;

namespace ShopNow.Infra.Migrations
{
    [Migration(6)]
    public class AddTableStock : Migration
    {
        public override void Up()
        {
            Create.Table("tb_stock_entry")
                .WithColumn("cd_stock").AsInt32().Identity().PrimaryKey()
                .WithColumn("id_item").AsInt32().ForeignKey()
                .WithColumn("operation").AsString(20).NotNullable()
                .WithColumn("count").AsInt32().NotNullable()
                .WithColumn("date").AsInt32().NotNullable();

            Create.ForeignKey("FK_Stock_Item")
                .FromTable("tb_stock_entry").ForeignColumn("id_item")
                .ToTable("tb_item").PrimaryColumn("cd_item");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_Stock_Item");
            Delete.Table("tb_stock_entry");
        }
    }
}
