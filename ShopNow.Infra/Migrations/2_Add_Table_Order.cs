﻿using FluentMigrator;

namespace ShopNow.Infra.Migrations
{
    [Migration(2)]
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
        }

        public override void Down()
        {
            Delete.Table("tb_order");
        }
    }
}
