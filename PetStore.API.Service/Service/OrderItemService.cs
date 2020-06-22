using PetStore.API.Service.Service.Interface;
using PetStore.Domain.Models;
using PetStore.OrderItem.Client.Client.Interface;
using System.Threading.Tasks;

namespace PetStore.API.Service.Service
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemClient _orderItemClient;

        public OrderItemService(IOrderItemClient orderItemClient)
        {
            _orderItemClient = orderItemClient;
        }

        public async Task OrderCreate(StockOrder stockOrder)
        {
            var result = await _orderItemClient.Send(stockOrder);
        }
    }
}