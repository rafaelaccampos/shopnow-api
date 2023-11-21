using FluentMigrator;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace ShopNow.Infra.Migrations
{
    public class AddTableColumnStatusInTableOrder : Migration
    {
        public override void Up()
        {
            Alter.Table("tb_order")
                .AddColumn("status").AsString(2);
        }

        public override void Down()
        {
            Delete.Column("status");
        }
    }
}
