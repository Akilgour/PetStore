using PetStore.Data.Repositorys.Interface;
using PetStore.Domain.Models;
using PetStore.Shared.QueMessages;
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
            var OrderResponse = new OrderResponse() { Success = true };

            foreach (var item in stockOrder.OrderItems)
            {
                var foo = await _stockItemRepository.GetByName(item.Name);

                if (item.Quantity <= foo.Quantity)
                {
                  
                }
                else
                {
                    OrderResponse.Success = false;
                }
            }

            if (OrderResponse.Success)
            {
                foreach (var item in stockOrder.OrderItems)
                {
                    var foo = await _stockItemRepository.GetByName(item.Name);

                    foo.Quantity = foo.Quantity - item.Quantity;

                    await _stockItemRepository.Update(foo);
                }
                OrderResponse.Message = "asdf";
            }
            else
            {
                OrderResponse.Message = $"Order {stockOrder.OrderNumber}, can not be placed not enought stock";
            }

            return OrderResponse;
        }
    }
}