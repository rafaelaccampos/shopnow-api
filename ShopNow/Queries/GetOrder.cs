using ShopNow.Dtos;
using ShopNow.Infra.Data;

namespace ShopNow.Queries
{
    public class GetOrder
    {
        private readonly IOrderDAO _orderDAO;

        public GetOrder(IOrderDAO orderDAO)
        {
            _orderDAO = orderDAO;
        }

        public async Task<GetOrderOutput> Execute(string code)
        {
           return new GetOrderOutput();
        }
    }
}
