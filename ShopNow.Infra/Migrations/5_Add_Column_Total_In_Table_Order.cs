using FluentMigrator;

namespace ShopNow.Infra.Migrations
{
    [Migration(5)]
    public class AddColumnTotalInTableOrder : Migration
    {
        public override void Up()
        {
            Alter.Table("tb_order")
                .AddColumn("total")
                .AsDecimal(18, 2)
                .Nullable();
        }
        public override void Down()
        {
            Delete.Column("total")
                .FromTable("tb_order");
        }
    }
}
