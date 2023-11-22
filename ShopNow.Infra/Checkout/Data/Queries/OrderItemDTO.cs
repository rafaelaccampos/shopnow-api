namespace ShopNow.Infra.Checkout.Data.Queries
{
    public class OrderItemDTO
    {
        public string Description { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
