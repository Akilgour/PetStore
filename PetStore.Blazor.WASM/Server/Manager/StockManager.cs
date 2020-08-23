using AutoMapper;
using PetStore.API.Service.Service.Interface;
using PetStore.Blazor.WASM.Server.Manager.Interface;
using PetStore.Blazor.WASM.Shared.Models;
using PetStore.Domain.Models;
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

        public async Task<StockItemCreateResult> Add(StockItemCreateCommand request)
        {
            return _mapper.Map<StockItemCreateResult>( await _stockManagerService.Add(_mapper.Map<StockItem>(request)));
        }

        public async Task Delete(Guid id)
        {
            await _stockManagerService.Delete(id);
        }

        public async Task<List<StockItemDisplay>> GetAll()
        {
            return _mapper.Map<List<StockItemDisplay>>(await _stockManagerService.GetAll());
        }

        public async Task<StockItemUpdate> GetById(Guid id)
        {
            return _mapper.Map<StockItemUpdate>(await _stockManagerService.GetById(id));
        }

        public async Task<StockItemUpdate> Update(StockItemUpdate request)
        {
            var stockItem = await _stockManagerService.GetById(request.Id);
            if (stockItem == null)
            {
                return null;
            }
            _mapper.Map(request, stockItem);
            await _stockManagerService.Update(stockItem);
            var result = _mapper.Map<StockItemUpdate>(stockItem);
            return result;
        }
    }
}