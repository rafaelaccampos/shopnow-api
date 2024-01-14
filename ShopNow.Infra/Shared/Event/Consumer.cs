using ShopNow.Domain.Shared.Handler;

namespace ShopNow.Infra.Shared.Event
{
    public class Consumer
    {
        public string EventName { get; set; }

        public IHandler Handler { get; set; }
    }
}
