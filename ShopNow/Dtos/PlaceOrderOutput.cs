using ShopNow.Domain.Entities;

namespace ShopNow.Dtos
{
    public class PlaceOrderOutput
    {
        private readonly Order _order;

        public PlaceOrderOutput(Order order)
        {
           _order = order;
        }

        public decimal Total => _order.GetTotal();
        public string OrderCode => _order.Code;
        public decimal Freight => _order.Freight;
    }
}
