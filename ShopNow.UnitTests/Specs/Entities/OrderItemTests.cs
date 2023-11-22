using FluentAssertions;
using ShopNow.Domain.Checkout.Entities;

namespace ShopNow.UnitTests.Specs.Entities
{
    public class OrderItemTests
    {
        [Test]
        public void ShouldBeAbleToCreateAnOrderItem()
        {
            var orderItem = new OrderItem(1, 200, 2);

            orderItem.GetTotal().Should().Be(400);
        }
    }
}
