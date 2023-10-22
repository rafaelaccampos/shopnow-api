namespace ShopNow.Domain.Entities
{
    public class Coupon
    {
        private Coupon()
        {

        }

        public Coupon(
            string code,
            decimal percentual,
            DateTime? expiredDate = default,
            DateTime actualDate = default)
        {
            Code = code;
            Percentual = percentual;
            ExpiredDate = expiredDate;
            ActualDate = actualDate;
        }

        public string Code { get; private set; }
        public decimal Percentual { get; private set; }
        public decimal Discount { get; private set; }
        public DateTime? ExpiredDate { get; private set; }
        public DateTime ActualDate { get; private set; }

        public void AddDiscount(decimal value)
        {
            var couponExpired = IsExpired(ActualDate);

            if(!couponExpired)
            {
                Discount = value * (Percentual / 100);
            }
        }

        public bool IsExpired(DateTime actualDate = new DateTime())
        {
            ActualDate = actualDate;

            if (ExpiredDate == null)
            {
                return false;
            }

            if (ActualDate == DateTime.MinValue)
            {
                ActualDate = DateTime.Now;
            }
            return ExpiredDate < ActualDate;
        }

        public bool IsValid(DateTime actualDate = new DateTime())
        {
            return !IsExpired(actualDate);
        }
    }
}
