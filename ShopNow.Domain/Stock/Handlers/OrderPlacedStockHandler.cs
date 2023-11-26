using ShopNow.Domain.Shared.Event;
using ShopNow.Domain.Shared.Handler;
using ShopNow.Domain.Stock.Repositories;

namespace ShopNow.Domain.Stock.Handlers
{
    public class OrderPlacedStockHandler : IHandler
    {
        private readonly IStockRepository _stockRepository;

        public OrderPlacedStockHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public Task Notify(IDomainEvent domainEvent)
        {
        }
    }
}
