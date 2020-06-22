using PetStore.Domain.Models;
using System.Threading.Tasks;

namespace PetStore.StockDelivery.Client.Client.Interface
{
    public interface IStockDeliveryClient
    {
        Task Send(StockItem stockItem);
    }
}