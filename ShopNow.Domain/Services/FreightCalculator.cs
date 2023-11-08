using ShopNow.Domain.Entities;

namespace ShopNow.Domain.Services
{
    public class FreightCalculator
    {
        public static decimal Calculate(Item item)
        {
            var freight = 1000 * item.GetVolume() * (item.GetDensity()/100);
            return (freight < 10) ? 10 : freight;
        }
    }
}
