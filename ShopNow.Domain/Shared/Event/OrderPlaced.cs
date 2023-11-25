namespace ShopNow.Domain.Shared.Event
{
    public class OrderPlaced : IDomainEvent
    {
        public OrderPlaced(string code, IList<ItemDto> items)
        {
            Code = code;
            Items = items;
            Name = "OrderPlaced";
        }

        public string Code { get; private set; }

        public string Name { get; private set; }
        
        public IList<ItemDto> Items { get; private set; }
    }
}
