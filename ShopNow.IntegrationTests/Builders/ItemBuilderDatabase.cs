using ShopNow.Domain.Checkout.Entities;
using ShopNow.Infra;
using ShopNow.IntegrationTests.Setup;
using ShopNow.Tests.Shared.Builders;

namespace ShopNow.IntegrationTests.Builders
{
    public class ItemBuilderDatabase : ItemBuilder
    {
        public override Item Generate(string ruleSets = null!)
        {
            var item = base.Generate();
            using var context = DatabaseBase.GetService<ShopContext>();
            context.AddRange(item);
            context.SaveChanges();
            return item;
        }
    }
}
