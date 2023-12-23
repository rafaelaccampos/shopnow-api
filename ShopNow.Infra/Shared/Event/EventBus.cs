using ShopNow.Domain.Shared.Event;

namespace ShopNow.Infra.Shared.Event
{
    public class EventBus
    {
        private IList<Consumer> _consumers = new List<Consumer>();

        public void Subscribe(Consumer consumer)
        {
            _consumers.Add(consumer);
        }

        public async virtual Task Publish(IDomainEvent domainEvent)
        {
            foreach (var consumer in _consumers)
            {
                if(consumer.EventName == domainEvent.Name) 
                {
                    await consumer.Handler.Notify(domainEvent);
                }
            }
        }
    }
}
