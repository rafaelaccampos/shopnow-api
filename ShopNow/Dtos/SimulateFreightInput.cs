namespace ShopNow.Dtos
{
    public class SimulateFreightInput
    {
        public ICollection<OrderItemInput> OrderItems { get; set; } = null!;
    }
}
