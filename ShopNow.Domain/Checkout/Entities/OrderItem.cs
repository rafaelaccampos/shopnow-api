﻿namespace ShopNow.Domain.Checkout.Entities
{
    public class OrderItem
    {
        public OrderItem(int idItem, decimal price, int count)
        {
            IdItem = idItem;
            Price = price;
            Count = count;
        }

        public int IdItem { get; private set; }
        public Item Item { get; private set; }
        public Order Order { get; private set; }
        public int IdOrder { get; private set; }
        public decimal Price { get; private set; }
        public int Count { get; private set; }

        public decimal GetTotal()
        {
            return Price * Count;
        }

    }
}
