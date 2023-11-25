using ShopNow.Domain.Shared.Event;

namespace ShopNow.Domain.Shared.Handler
{
    public interface IHandler
    {
        Task Notify(IDomainEvent domainEvent);
    }
}
