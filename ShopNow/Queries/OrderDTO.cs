﻿namespace ShopNow.Queries
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Cpf { get; set; }
        public decimal Freight { get; set; }
        public decimal Total { get; set; }
    }
}
