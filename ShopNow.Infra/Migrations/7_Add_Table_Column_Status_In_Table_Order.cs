using FluentMigrator;

namespace ShopNow.Infra.Migrations
{
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
