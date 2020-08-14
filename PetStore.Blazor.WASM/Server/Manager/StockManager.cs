using AutoMapper;
using PetStore.API.Service.Service.Interface;
using PetStore.Blazor.WASM.Server.Manager.Interface;
using PetStore.Blazor.WASM.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Server.Manager
{
    public class StockManager : BaseManager, IStockManager
    {
        private readonly IStockManagerService _stockManagerService;

        public StockManager(IStockManagerService stockManagerService, IMapper mapper)
            : base(mapper)
        {
            _stockManagerService = stockManagerService ?? throw new System.ArgumentNullException(nameof(stockManagerService));
        }

        public async Task<List<StockItemDisplay>> GetAll()
        {
            return _mapper.Map<List<StockItemDisplay>>(await _stockManagerService.GetAll());
        }

        public async Task<StockItemUpdate> GetById(Guid id)
        {
            return _mapper.Map<StockItemUpdate>(await _stockManagerService.GetById(id));
        }
    }
}