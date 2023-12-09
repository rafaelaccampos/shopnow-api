namespace ShopNow.Domain.Checkout.Entities
{
    public class Order
    {
        private Order()
        {
            Cpf = new Cpf();
        }

        public Order(string rawCpf, DateTime issueDate = new DateTime(), int sequence = 1)
        {
            Code = new OrderCode(sequence, issueDate).GenerateCode();
            Cpf = new Cpf();
            IssueDate = issueDate;
            Freight = 0;
            Sequence = sequence;

            if (!Cpf.Validate(rawCpf))
            {
                throw new InvalidOperationException("Cpf is invalid!");
            }
            CpfNumber = rawCpf;
        }

        public int Id { get; private set; }

        public string Code { get; private set; }
        
        public Cpf? Cpf { get; private set; }
        
        public string CpfNumber { get; private set; }
        
        
        public Coupon Coupon { get; private set; }
        
        public string? IdCoupon { get; private set; }
        
        public DateTime IssueDate { get; private set; }
        
        public decimal Freight { get; private set; }
        
        public string? Status { get; private set; }
        
        public int Sequence { get; private set; }
        
        public ICollection<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();

        public void AddItem(Item item, int count)
        {
            OrderItems.Add(new OrderItem(item.Id, item.Price, count));
        }

        public void AddFreight(decimal freight)
        {
            Freight = freight;
        }

        public void AddCoupon(Coupon coupon)
        {
            if (coupon.IsExpired())
            {
                return;
            }

            Coupon = coupon;
        }
        public void Cancel()
        {
            Status = "Cancelled";
        }

        public decimal GetTotal()
        {
            decimal total = 0;

            foreach (var orderItem in OrderItems)
            {
                total += orderItem.GetTotal();
            }

            if (Coupon != null)
            {
                Coupon.AddDiscount(total);
                total -= Coupon.Discount;
            }

            return total;
        }
    }
}
