using PetStore.Blazor.WASM.Shared.Models;
using System.Threading.Tasks;

namespace PetStore.API.Manager.Interface
{
    public interface IStockDeliveryManager
    {
        Task AddStock(StockDeliveryCreate stockDeliveryCreate);
    }
}