using FluentMigrator;

namespace ShopNow.Infra.Migrations
{
    [Migration(1)]
    public class AddTableItem : Migration
    {
        public override void Up()
        {
            Create.Table("tb_item")
                .WithColumn("cd_item").AsInt32().PrimaryKey().Identity()
                .WithColumn("description").AsString(50).NotNullable()
                .WithColumn("category").AsString(50).NotNullable()
                .WithColumn("price").AsDecimal(18, 2).NotNullable()
                .WithColumn("width").AsDecimal(18, 2).NotNullable()
                .WithColumn("height").AsDecimal(18, 2).NotNullable()
                .WithColumn("length").AsDecimal(18, 2).NotNullable()
                .WithColumn("weight").AsDecimal(18, 2).NotNullable();
        }

        public override void Down()
        {
            Delete.Table("tb_item");
        }
    }
}
