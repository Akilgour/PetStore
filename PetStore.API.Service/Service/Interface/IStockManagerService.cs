using PetStore.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetStore.API.Service.Service.Interface
{
    public interface IStockManagerService
    {
        Task<List<StockItem>> GetAll();
    }
}
