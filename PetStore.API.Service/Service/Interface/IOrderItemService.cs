using PetStore.Domain.Models;
using System.Threading.Tasks;

namespace PetStore.API.Service.Service.Interface
{
    public interface IOrderItemService
    {
        Task OrderCreate(StockOrder stockOrder);
    }
}