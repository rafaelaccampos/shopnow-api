using Bogus.Extensions.Brazil;
using FluentAssertions.Execution;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using ShopNow.Domain.Checkout.Entities;
using ShopNow.Domain.Checkout.Factory;
using ShopNow.Domain.Checkout.Repositories;
using ShopNow.Domain.Shared.Event;
using ShopNow.Infra.Shared.Event;
using ShopNow.Tests.Shared.Builders;
using ShopNow.UnitTests.Setup;
using ShopNow.UseCases;

namespace ShopNow.UnitTests.Specs.UseCases
{
    public class CancelOrderTests : BaseTest
    {
        private IAbstractRepositoryFactory _abstractRepositoryFactory;
        private EventBus _eventBus;
        private IOrderRepository _orderRepository;

        [SetUp] 
        public void SetUp() 
        {
            _abstractRepositoryFactory = Substitute.For<IAbstractRepositoryFactory>();
            _eventBus = Substitute.For<EventBus>();
            _orderRepository = Substitute.For<IOrderRepository>();
            _abstractRepositoryFactory.CreateOrderRepository().Returns(_orderRepository);
        }

        [Test]
        public async Task ShouldBeAbleToReceiveCallingsToUpdateOrderAndForEventCancelOrderWhenOrderIsCancelled()
        {
            var cpf = Faker.Person.Cpf(false);
            var issueDate = DateTime.Now;

            var item = new ItemBuilder()
                .WithId(1)
                .WithCategory("Guitarra")
                .WithDescription("Eletrônicos")
                .WithPrice(1000)
                .WithWidth(100)
                .WithHeight(50)
                .WithLength(15)
                .WithHeight(3)
                .Generate();

            var order = new Order(cpf, issueDate);
            order.AddItem(item, 1);
            var orderItems = order.OrderItems
                .Select(o => 
                new ItemDto { 
                    Id = o.IdItem, 
                    Count = o.Count
                });
            _orderRepository.Get(order.Code).Returns(order);

            var cancelOrder = new CancelOrder(_abstractRepositoryFactory, _eventBus);
            await cancelOrder.Execute(order.Code);

            using(new AssertionScope())
            {
                await _orderRepository.Received(1).Update(order);
                await _eventBus.Received(1)
                    .Publish(Arg.Is<OrderCancelled>(
                        o => o.Code == order.Code && o.Items.SequenceEqual(orderItems)));
            }
        }

        [Test]
        public async Task ShouldNotBeAbleToReceiveCallingsWhenOrderDoesNotExists()
        {
            var code = Faker.Random.String2(20);
            _orderRepository.Get(code).ReturnsNull();

            var cancelOrder = new CancelOrder(_abstractRepositoryFactory, _eventBus);
            await cancelOrder.Execute(code);

            using (new AssertionScope())
            {
                await _orderRepository.DidNotReceive().Update(null);
                await _eventBus.DidNotReceive()
                    .Publish(Arg.Is<OrderCancelled>(
                        o => o.Code == code && o.Items.SequenceEqual(null!)));
            }
        }
    }
}
