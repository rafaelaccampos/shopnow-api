namespace ShopNow.Domain.Stock.Entities
{
    public class StockEntry
    {
        public StockEntry(int idItem, string operation, int quantity)
        {
            IdItem = idItem;
            Operation = operation;
            Quantity = quantity;
        }

        public int IdItem { get; private set; }

        public string Operation { get; private set; }

        public int Quantity { get; private set; }
    }
}
