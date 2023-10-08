using Bogus.Extensions.Brazil;
using FluentAssertions;
using FluentAssertions.Execution;
using ShopNow.Domain.Entities;
using ShopNow.Domain.Repositories;
using ShopNow.Dtos;
using ShopNow.IntegrationTests.Setup;
using ShopNow.UseCases;

namespace ShopNow.IntegrationTests.Specs.Repositories
{
    public class PlaceOrderTests : DatabaseBase
    {
        [Test]
        public async Task ShouldBeAbleToPlaceOrder()
        {
            var placeOrderInput = new PlaceOrderInput
            {
                Cpf = "18731465072",
                OrderItems = new List<OrderItemInput>
                {
                    new OrderItemInput
                    {
                        IdItem = 1,
                        Count = 1,
                    },
                    new OrderItemInput
                    {
                        IdItem = 2,
                        Count = 1,
                    },
                    new OrderItemInput
                    {
                        IdItem = 3,
                        Count = 3,
                    }
                },
                IssueDate = new DateTime(2023, 07, 30)
            };

            var items = new List<Item>
            {
                new Item(1, "Guitarra", "Eletrônicos", 1000, 100, 50, 15, 3),
                new Item(2, "Amplificador", "Eletrônicos", 5000, 50, 50, 50, 22),
                new Item(3, "Cabo", "Eletrônicos", 30, 10, 10, 10, 1),
            };

            _context.Items.AddRange(items);
           await _context.SaveChangesAsync();

            var itemRepository = GetService<IItemRepository>();
            var orderRepository = GetService<IOrderRepository>();
            var couponRepository = GetService<ICouponRepository>();

            var placeOrder = new PlaceOrder(itemRepository, orderRepository, couponRepository);
            var output = await placeOrder.Execute(placeOrderInput);

            using (new AssertionScope())
            {
                output.Total.Should().Be(6090);
                output.OrderCode.Should().Be("202300000001");
            }

        }

        [Test]
        public async Task ShouldBeAbleToPlaceOrderWithAnCoupon()
        {
            var placeOrderInput = new PlaceOrderInput()
            {
                Cpf = Faker.Person.Cpf(false),
                OrderItems = new List<OrderItemInput>
                {
                    new OrderItemInput
                    {
                        IdItem = 1,
                        Count = 1,
                    },
                    new OrderItemInput
                    {
                        IdItem = 2,
                        Count = 1,
                    },
                    new OrderItemInput
                    {
                        IdItem = 3,
                        Count = 3,
                    }
                },
                IssueDate = new DateTime(2023, 07, 30),
                Coupon = "VALE20",
            };

            var items = new List<Item>()
            {
                new Item(1, "Guitarra", "Eletrônicos", 1000, 100, 50, 15, 3),
                new Item(2, "Amplificador", "Eletrônicos", 5000, 50, 50, 50, 22),
                new Item(3, "Cabo", "Eletrônicos", 30, 10, 10, 10, 1),
            };

            _context.Items.AddRange(items);

            var coupon = new Coupon("VALE20", 20);
            _context.Coupons.Add(coupon);

            await _context.SaveChangesAsync();

            var itemRepository = GetService<IItemRepository>();
            var orderRepository = GetService<IOrderRepository>();
            var couponRepository = GetService<ICouponRepository>();
            
            var placeOrder = new PlaceOrder(itemRepository, orderRepository, couponRepository);
            var output = await placeOrder.Execute(placeOrderInput);

            using (new AssertionScope())
            {
                output.Total.Should().Be(4872);
                output.OrderCode.Should().Be("202300000001");
            }
        }
    }
}
