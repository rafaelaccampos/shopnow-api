using ShopNow.Domain.Checkout.Entities;

namespace ShopNow.Domain.Stock.Entities
{
    public class StockEntry
    {
        public StockEntry(int idItem, string operation, int count)
        {
            IdItem = idItem;
            Operation = operation;
            Count = count;
        }

        public int Id { get; private set; }

        public int IdItem { get; private set; }

        public Item Item { get; private set; } = null!;

        public string Operation { get; private set; }

        public int Count { get; private set; }

        public DateTime Date { get; set; }
    }
}
