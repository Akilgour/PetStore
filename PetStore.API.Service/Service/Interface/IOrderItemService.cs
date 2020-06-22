using PetStore.Domain.Models;
using PetStore.Shared.QueMessages;
using System.Threading.Tasks;

namespace PetStore.API.Service.Service.Interface
{
    public interface IOrderItemService
    {
        Task<OrderResponse> OrderCreate(StockOrder stockOrder);
    }
}