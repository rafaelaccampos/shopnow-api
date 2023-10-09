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
            DateTime? actualDate = default)
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
        public DateTime? ActualDate { get; private set; }

        public void AddDiscount(decimal value)
        {
            var couponExpired = IsExpired();

            if(!couponExpired)
            {
                Discount = value * (Percentual / 100);
            }
        }

        public bool IsExpired()
        {
            if(ExpiredDate != null && ExpiredDate < ActualDate)
            {
                return true;
            }
            return false;
        }

        public bool IsValid()
        {
            return !IsExpired();
        }
    }
}
