using PetStore.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetStore.Data.Repositorys.Interface
{
    public interface IStockItemRepository : IRepository<StockItem>
    {
        void UpdateDontSave(StockItem stockItem);

        Task<StockItem> GetByName(string name);

        Task SaveChangesAsync();

        Task<List<StockItem>> GetAll();
    }
}