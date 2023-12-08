using FluentMigrator;

namespace ShopNow.Infra.Migrations
{
    [Migration(7)]
    public class AddTableColumnStatusInTableOrder : Migration
    {
        public override void Up()
        {
            Alter.Table("tb_order")
                .AddColumn("status").AsString(20).Nullable();
        }

        public override void Down()
        {
            Delete.Column("status");
        }
    }
}
