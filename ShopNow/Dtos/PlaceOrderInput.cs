using ShopNow.Domain.Entities;

namespace ShopNow.Dtos
{
    public class PlaceOrderInput
    {
        public string Cpf { get; set; }
        public IList<OrderItem> OrderItems { get; set; }
        public DateTime IssueDate { get; set; }
        public string Coupon { get; set; }
    }
}
