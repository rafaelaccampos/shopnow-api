using NSubstitute;
using ShopNow.Domain.Shared.Event;
using ShopNow.Domain.Stock.Entities;
using ShopNow.Domain.Stock.Handlers;
using ShopNow.Domain.Stock.Repositories;
using ShopNow.UnitTests.Setup;

namespace ShopNow.UnitTests.Specs.Handlers
{
    public class OrderPlacedStockHandlerTests : BaseTest
    {
        private IStockRepository _stockRepository;
        [SetUp]
        public void SetUp()
        {
            _stockRepository = Substitute.For<IStockRepository>();
        }

        [Test]
        public async Task ShouldBeAbleToUnstockAnItemAndSave()
        {
            var code = Faker.Random.String2(20);
            var idItem = Faker.Random.Int(1, 2);
            var count = Faker.Random.Int(1, 1);
            var items = new List<ItemDto>
            {
                new ItemDto
                {
                    Id = idItem,
                    Count = count,
                }
            };

            var orderPlaced = new OrderPlaced(code, items);
            var orderPlacedStockHandler = new OrderPlacedStockHandler(_stockRepository);
            await orderPlacedStockHandler.Notify(orderPlaced);

            var stockEntry = new StockEntry(idItem, "in", count);
            await _stockRepository.Received(1).Save(Arg.Is<StockEntry>(s => s.IdItem == idItem && s.Count == count));
        }
    }
}
