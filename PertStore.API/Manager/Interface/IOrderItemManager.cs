using PetStore.Blazor.WASM.Shared.Models;
using System.Threading.Tasks;

namespace PetStore.API.Manager.Interface
{
    public interface IOrderItemManager
    {
        Task<StockOrderOrderResponse> OrderCreate(StockOrderCreate stockOrderCreate);
    }
}
