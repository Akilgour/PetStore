using PetStore.Domain.Models;
using System.Threading.Tasks;

namespace PetStore.Data.Repositorys.Interface
{
    public interface IStockItemRepository
    {
        Task Create(StockItem stockItem);
    }
}