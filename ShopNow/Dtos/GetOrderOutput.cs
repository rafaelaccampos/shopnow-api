using ShopNow.Domain.Entities;
using ShopNow.Queries;

namespace ShopNow.Dtos
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
