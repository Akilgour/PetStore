using PetStore.Domain.Models;
using PetStore.Shared.Helpers;
using PetStore.Shared.QueMessages;
using System.Threading.Tasks;

namespace PetStore.OrderItem.Client
{
    public class OrderItemClient
    {
        public async Task<OrderResponse> Publish(StockOrder stockOrder)
        {
            using (var rpcClient = new RpcClient())
            {
                var response = await rpcClient.SendAsync(stockOrder.Serialize());
                return response;
            }
        }
    }
}