namespace ShopNow.Domain.Entities
{
    public class OrderCode
    {
        public OrderCode(int sequence, DateTime date)
        {
            Sequence = sequence;
            Date = date;
        }

        public int Sequence { get; private set; }
        public DateTime Date { get; private set; }

        public string GenerateCode()
        {
            var sequenceEightChar = $"{Sequence}".PadLeft(8, '0');
            return $"{Date.Year}{sequenceEightChar}";
        }

    }
}
