using PetStore.Blazor.WASM.Shared.Models;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Server.Manager.Interface
{
    public interface IOrderItemManager
    {
        Task<StockOrderOrderResponse> OrderCreate(StockOrderCreate stockOrderCreate);
    }
}