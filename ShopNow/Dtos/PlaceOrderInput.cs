namespace ShopNow.Dtos
{
    public class PlaceOrderInput
    {
        public string? Cpf { get; set; }
        public ICollection<OrderItemInput> OrderItems { get; set; } = null!;
        public DateTime IssueDate => DateTime.Now;
        public string? Coupon { get; set; }
    }
}
