namespace ShopNow.Domain.Entities
{
    public class Order
    {
        private IList<OrderItem> _orderItems = new List<OrderItem>();

        public Order(string rawCpf, DateTime issueDate = new DateTime(), int sequence = 1)
        {
            Code = new OrderCode(sequence, issueDate).GenerateCode();
            Cpf = new Cpf();
            IssueDate = issueDate;
            Freight = 0;

            if (!Cpf.Validate(rawCpf))
            {
                throw new InvalidOperationException("Cpf is invalid!");
            }
        }

        public string Code { get; private set; }
        public Cpf Cpf { get; private set; }
        public Coupon Coupon { get; private set; }
        public DateTime IssueDate { get; private set; }
        public decimal Freight { get; private set; }

        public void AddItem(Item item, int count)
        {
            Freight += item.GetFreight() * count;
            _orderItems.Add(new OrderItem(item.Id, item.Price, count));
        }

        public void AddCoupon(Coupon coupon)
        {
            if (coupon.IsExpired())
            {
                return;
            }

            Coupon = coupon;
        }

        public decimal GetTotal()
        {
            decimal total = 0;

            foreach (var orderItem in _orderItems)
            {
                total += orderItem.GetTotal();
            }

            if (Coupon != null)
            {
                Coupon.AddDiscount(total);
                total -= Coupon.GetDiscount();
            }

            return total;
        }

    }
}
