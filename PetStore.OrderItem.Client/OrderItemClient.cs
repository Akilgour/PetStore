using PetStore.Domain.Models;
using PetStore.Shared.Helpers;
using PetStore.Shared.QueMessages;
using System.Threading.Tasks;

namespace PetStore.OrderItem.Client
{
    public class OrderItemClient
    {

        private static async Task<OrderResponse> Publish(StockItem stockItem)
        {
            using (var rpcClient = new RpcClient())
            {
                var response = await rpcClient.SendAsync(stockItem.Serialize());
                return response;
            }
        }
    }
}