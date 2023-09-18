namespace ShopNow.Domain.Entities
{
    public class Item
    {
        public Item(
            string description,
            string category,
            decimal price,
            decimal width = 0,
            decimal height = 0,
            decimal length = 0,
            decimal weight = 0)
        {
            Description = description;
            Category = category;
            Price = price;
            Width = width;
            Height = height;
            Length = length;
            Weight = weight;
        }

        public int Id { get; set; }
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
            var freight = 1000 * GetVolume() * (GetDensity() / 100);

            return freight < 10 ? 10 : freight;
        }
    }
}
