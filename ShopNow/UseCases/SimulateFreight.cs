﻿using ShopNow.Domain.Repositories;
using ShopNow.Dtos;

namespace ShopNow.UseCases
{
    public class SimulateFreight
    {
        private readonly IItemRepository _itemRepository;

        public SimulateFreight(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<decimal> Execute(SimulateFreightInput simulateFreightInput)
        {
            decimal freight = 0;

            foreach(var orderItem in simulateFreightInput.OrderItems)
            {
                var item = await _itemRepository.FindByIdAsync(orderItem.IdItem);
                freight += item.GetFreight() * orderItem.Count;
            }

            return freight;
        }
    }
}
