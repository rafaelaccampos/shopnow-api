using ShopNow.Domain.Checkout.Entities;

namespace ShopNow.Dtos
{
    public class PlaceOrderOutput
    {
        public PlaceOrderOutput(Order order)
        {
            Total = order.GetTotal();
            OrderCode = order.Code;
            Freight = order.Freight;
        }
        public decimal? Total { get; set; }

        public string? OrderCode { get; set; }

        public decimal? Freight { get; set; }
    }
}
