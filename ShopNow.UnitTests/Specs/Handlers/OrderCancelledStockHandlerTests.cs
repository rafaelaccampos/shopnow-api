using NSubstitute;
using ShopNow.Domain.Shared.Event;
using ShopNow.Domain.Stock.Entities;
using ShopNow.Domain.Stock.Handlers;
using ShopNow.Domain.Stock.Repositories;
using ShopNow.UnitTests.Setup;

namespace ShopNow.UnitTests.Specs.Handlers
{
    public class OrderCancelledStockHandlerTests : BaseTest
    {
        private IStockRepository _stockRepository;

        [SetUp]
        public void Setup()
        {
            _stockRepository = Substitute.For<IStockRepository>();
        }

        [Test]
        public async Task ShouldBeAbleToReturnItemFromStockAndSave()
        {
            var idItem = Faker.Random.Int(1, 10);
            var count = Faker.Random.Int(1, 2);
            var code = Faker.Random.String2(20);
            var items = new List<ItemDto>
            {
                new ItemDto
                {
                    Id = idItem,
                    Count = count,
                }
            };
            var orderCancelled = new OrderCancelled(code, items);
            var orderCancelledStockHandler = new OrderCancelledStockHandler(_stockRepository);
            await orderCancelledStockHandler.Notify(orderCancelled);

            var stockEntry = new StockEntry(idItem, "out", count);
            await _stockRepository.Received(1).Save(Arg.Is<StockEntry>(s => s.IdItem == idItem && s.Count == count));
        }
    }
}
