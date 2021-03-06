﻿using PetStore.Data.Repositorys.Interface;
using PetStore.Domain.Models;
using PetStore.OrderItem.Manager.Manager.Interface;
using PetStore.Shared.QueMessages;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.OrderItem.Manager.Manager
{
    public class OrderItemManager : IOrderItemManager
    {
        private readonly IStockItemRepository _stockItemRepository;

        public OrderItemManager(IStockItemRepository stockItemRepository)
        {
            _stockItemRepository = stockItemRepository;
        }

        public async Task<OrderResponse> Order(StockOrder stockOrder)
        {
            var orderResponse = new OrderResponse() { Success = true };
            var itemsThatDontPass = new List<string>();

            foreach (var orderItem in stockOrder.OrderItems)
            {
                var stockItem = await _stockItemRepository.GetByName(orderItem.Name);
                if (orderItem.Quantity <= stockItem.Quantity)
                {
                    stockItem.Quantity -= orderItem.Quantity;
                    _stockItemRepository.UpdateDontSave(stockItem);
                }
                else
                {
                    itemsThatDontPass.Add(orderItem.Name);
                    orderResponse.Success = false;
                }
            }

            if (orderResponse.Success)
            {
                orderResponse.Message = $"Success Ordered {stockOrder.OrderNumber}";
                await _stockItemRepository.SaveChangesAsync();
            }
            else
            {
                orderResponse.Message = $"Order {stockOrder.OrderNumber}, can not be placed not enought stock {string.Join(", ", itemsThatDontPass.ToArray())}.";
            }
            return orderResponse;
        }
    }
}