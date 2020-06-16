using PetStore.Domain.Models;
using System.Threading.Tasks;

namespace PetStore.StockDelivery.Manager.Managers.Interface
{
    public interface IStockDeliveryManager
    {
        Task Create(StockItem stockItem);
    }
}
