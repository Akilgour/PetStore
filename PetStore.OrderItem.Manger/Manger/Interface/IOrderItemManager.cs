using PetStore.Domain.Models;
using PetStore.Shared.QueMessages;
using System.Threading.Tasks;

namespace PetStore.OrderItem.Manager.Manager.Interface
{
    public interface IOrderItemManager
    {
        Task<OrderResponse> Order(StockOrder stockOrder);
    }
}