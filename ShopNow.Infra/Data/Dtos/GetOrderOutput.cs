using ShopNow.Infra.Data.Queries;

namespace ShopNow.Infra.Data.Dtos
{
    public class GetOrderOutput
    {
        public string Code { get; set; }
        public string Cpf { get; set; }
        public IList<ItemDTO> Items { get; set; }
        public decimal Freight { get; set; }
        public decimal Total { get; set; }
    }
}
