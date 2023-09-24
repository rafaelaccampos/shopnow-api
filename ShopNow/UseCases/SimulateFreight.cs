using ShopNow.Dtos;

namespace ShopNow.UseCases
{
    public class SimulateFreight
    {
        private readonly PlaceOrder _placeOrder;

        public SimulateFreight(PlaceOrder placeOrder)
        {
            _placeOrder = placeOrder;
        }

        public async Task<decimal> Execute(PlaceOrderInput placeOrderInput)
        {
            var order = await _placeOrder.Execute(placeOrderInput);

            return order.Freight;
        }
    }
}
