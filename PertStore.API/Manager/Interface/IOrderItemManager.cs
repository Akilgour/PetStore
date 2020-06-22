using PetStore.Shared.DTO;
using System.Threading.Tasks;

namespace PetStore.API.Manager.Interface
{
    public interface IOrderItemManager
    {
        Task OrderCreate(StockOrderCreate stockOrderCreate);
    }
}
