using PetStore.Domain.Models;
using PetStore.Shared.QueMessages;
using System.Threading.Tasks;

namespace PetStore.OrderItem.Client.Client.Interface
{
    public interface IOrderItemClient
    {
        Task<OrderResponse> Send(StockOrder stockOrder);
    }
}