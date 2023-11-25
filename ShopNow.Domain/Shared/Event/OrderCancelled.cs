namespace ShopNow.Domain.Shared.Event
{
    public class OrderCancelled : IDomainEvent
    {
        public OrderCancelled(string code, IList<ItemDto> items)
        {
            Code = code;
            Items = items;
            Name = "OrderCancelled";
        }

        public string Code { get; private set; }

        public string Name { get; private set; }

        public IList<ItemDto> Items { get; private set; }
    }
}
