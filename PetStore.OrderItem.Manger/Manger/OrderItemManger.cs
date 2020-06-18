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

            foreach (var item in stockOrder.OrderItems)
            {
                var foo = await _stockItemRepository.GetByName(item.Name);

                if (item.Quantity <= foo.Quantity)
                {
                  
                }
                else
                {
                    itemsThatDontPass.Add(item.Name);
                    orderResponse.Success = false;
                }
            }

            if (orderResponse.Success)
            {
                foreach (var item in stockOrder.OrderItems)
                {
                    var foo = await _stockItemRepository.GetByName(item.Name);

                    foo.Quantity = foo.Quantity - item.Quantity;

                    await _stockItemRepository.Update(foo);
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