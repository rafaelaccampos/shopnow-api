using ShopNow.Domain.Shared.Handler;

namespace ShopNow.Infra.Shared.Event
{
    public class Consumer
    {
        public string EventName { get; set; }

        public Func<IHandler> HandlerFactory { get; set; }
    }
}
