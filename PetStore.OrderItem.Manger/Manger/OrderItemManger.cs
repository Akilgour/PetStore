using PetStore.Data.Repositorys.Interface;
using PetStore.Domain.Models;
using PetStore.Shared.QueMessages;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.OrderItem.Manger.Manger
{
    public class OrderItemManger
    {
        private readonly IStockItemRepository _stockItemRepository;

        public OrderItemManger(IStockItemRepository stockItemRepository)
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
                  
                }
                else
                {
                    itemsThatDontPass.Add(orderItem.Name);
                    orderResponse.Success = false;
                }
            }

            if (orderResponse.Success)
            {
                foreach (var orderItem in stockOrder.OrderItems)
                {
                    var stockItem = await _stockItemRepository.GetByName(orderItem.Name);
                    stockItem.Quantity -= orderItem.Quantity;
                    await _stockItemRepository.Update(stockItem);
                }
                orderResponse.Message = $"Success Ordered {stockOrder.OrderNumber}";
            }
            else
            {
                orderResponse.Message = $"Order {stockOrder.OrderNumber}, can not be placed not enought stock {string.Join(", ", itemsThatDontPass.ToArray())}.";
            }
            return orderResponse;
        }
    }
}