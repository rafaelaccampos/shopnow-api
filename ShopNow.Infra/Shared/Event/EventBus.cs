using ShopNow.Domain.Shared.Event;

namespace ShopNow.Infra.Shared.Event
{
    public class EventBus
    {
        public IList<Consumer> Consumers { get; private set; }

        public EventBus()
        {
            Consumers = new List<Consumer>();
        }

        public void Subscribe(Consumer consumer)
        {
            Consumers.Add(consumer);
        }

        public async Task Publish(IDomainEvent domainEvent)
        {
            foreach (var consumer in Consumers)
            {
                if(consumer.EventName == domainEvent.Name) 
                {
                    await consumer.Handler.Notify(domainEvent);
                }
            }
        }
    }
}
