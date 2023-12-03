using ShopNow.Domain.Shared.Event;
using ShopNow.Domain.Shared.Handler;
using ShopNow.Domain.Stock.Entities;
using ShopNow.Domain.Stock.Repositories;

namespace ShopNow.Domain.Stock.Handlers
{
    public class OrderCancelledStockHandler : IHandler
    {
        private readonly IStockRepository _stockRepository;

        public OrderCancelledStockHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task Notify(IDomainEvent domainEvent)
        {
            var orderCancelled = (OrderCancelled)domainEvent;

            foreach(var orderItem in orderCancelled.Items)
            {
                var stockEntry = new StockEntry(orderItem.Id, "out", orderItem.Count);
                await _stockRepository.Save(stockEntry);
            }
        }
    }
}
