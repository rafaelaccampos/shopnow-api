namespace ShopNow.Domain.Shared.Event
{
    public class ItemDto
    {
        public int Id { get; set; }

        public int Count { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is not ItemDto item) return false;
            return item.Id == Id && item.Count == Count;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Count);
        }
    }
}
