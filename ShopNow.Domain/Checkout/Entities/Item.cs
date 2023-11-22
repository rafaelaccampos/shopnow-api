using ShopNow.Domain.Checkout.Services;

namespace ShopNow.Domain.Checkout.Entities
{
    public class Item
    {
        public Item(
            int id,
            string description,
            string category,
            decimal price,
            decimal width = 0,
            decimal height = 0,
            decimal length = 0,
            decimal weight = 0)
        {
            Id = id;
            Description = description;
            Category = category;
            Price = price;
            Width = width;
            Height = height;
            Length = length;
            Weight = weight;
        }

        public int Id { get; private set; }
        public string Description { get; private set; }
        public string Category { get; private set; }
        public decimal Price { get; private set; }
        public decimal Width { get; private set; }
        public decimal Height { get; private set; }
        public decimal Length { get; private set; }
        public decimal Weight { get; private set; }

        public decimal GetVolume()
        {
            return Width / 100 * Height / 100 * Length / 100;
        }

        public decimal GetDensity()
        {
            return Weight / GetVolume();
        }

        public decimal GetFreight()
        {
            return FreightCalculator.Calculate(this);
        }
    }
}
