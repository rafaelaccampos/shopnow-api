using ShopNow.Infra.Data.Dao;
using ShopNow.Infra.Data.Dtos;

namespace ShopNow.Infra.Data.Queries
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
