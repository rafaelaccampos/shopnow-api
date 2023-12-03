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

        public int IdItem { get; private set; }

        public string Operation { get; private set; }

        public int Count { get; private set; }
    }
}
