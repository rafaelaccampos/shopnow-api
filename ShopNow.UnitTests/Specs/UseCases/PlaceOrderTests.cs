using Bogus.Extensions.Brazil;
using FluentAssertions.Execution;
using NSubstitute;
using ShopNow.Controllers;
using ShopNow.Domain.Checkout.Entities;
using ShopNow.Domain.Checkout.Factory;
using ShopNow.Domain.Checkout.Repositories;
using ShopNow.Domain.Shared.Event;
using ShopNow.Dtos;
using ShopNow.Infra.Shared.Event;
using ShopNow.Tests.Shared.Builders;
using ShopNow.UnitTests.Setup;
using ShopNow.UseCases;

namespace ShopNow.UnitTests.Specs.UseCases
{
    public class PlaceOrderTests : BaseTest
    {
        private IItemRepository _itemRepository;
        private IOrderRepository _orderRepository;
        private ICouponRepository _couponRepository;
        private IAbstractRepositoryFactory _abstractRepositoryFactory;
        private EventBus _eventBus;

        [SetUp]
        public void SetUp()
        {
            _itemRepository = Substitute.For<IItemRepository>();
            _orderRepository = Substitute.For<IOrderRepository>();
            _couponRepository = Substitute.For<ICouponRepository>();
            _abstractRepositoryFactory = Substitute.For<IAbstractRepositoryFactory>();
            _eventBus = Substitute.For<EventBus>();

            _abstractRepositoryFactory.CreateItemRepository().Returns(_itemRepository);
            _abstractRepositoryFactory.CreateOrderRepository().Returns(_orderRepository);
            _abstractRepositoryFactory.CreateCouponRepository().Returns(_couponRepository);
        }

        [Test]
        public async Task ShouldBeAbleToCallSaveOrderAndEventPlaceOrderWhenOrderIsPlaced()
        {
            var placeOrderInput = new PlaceOrderInput
            {
                Cpf = Faker.Person.Cpf(false),
                OrderItems = new List<OrderItemInput>
                {
                    new OrderItemInput
                    {
                        IdItem = 1,
                        Count = 1,
                    },
                },
                IssueDate = DateTime.Now,
                Coupon = "VALE20"
            };
            var item = new ItemBuilder()
                .WithId(1)
                .WithDescription("Guitarra")
                .WithCategory("Eletrônicos")
                .WithPrice(1000)
                .WithWidth(100)
                .WithHeight(10)
                .WithLength(15)
                .WithWeight(3)
                .Generate();

            var coupon = new CouponBuilder()
                .WithCode("VALE20")
                .WithPercentual(20)
                .Generate();

            var order = new Order(placeOrderInput.Cpf, placeOrderInput.IssueDate);
            order.AddItem(item, 1);

            _orderRepository.Count().Returns(0);
            _itemRepository.FindByIdAsync(placeOrderInput.OrderItems.First().IdItem).Returns(item);
            _couponRepository.FindByCode(placeOrderInput.Coupon).Returns(coupon);
            await _orderRepository.Save(order);

            var placeOrder = new PlaceOrder(_abstractRepositoryFactory, _eventBus);
            var placeOrderOutput = await placeOrder.Execute(placeOrderInput);

            var orderItemsDto = order.OrderItems.Select(o =>
                new ItemDto
                {
                    Id = placeOrderInput.OrderItems.First().IdItem,
                    Count = placeOrderInput.OrderItems.First().Count,
                }).ToList();

            using (new AssertionScope())
            {
                await _orderRepository.Received(1).Save(order);
                await _eventBus.Received(1).Publish(Arg.Is<OrderPlaced>(o => o.Code == order.Code));
            }
        }
    }
}
