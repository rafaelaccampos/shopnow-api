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
    }
}
