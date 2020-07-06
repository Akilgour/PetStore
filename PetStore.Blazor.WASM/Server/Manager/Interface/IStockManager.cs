﻿using PetStore.Blazor.WASM.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Server.Manager.Interface
{
    public interface IStockManager
    {
        Task<List<StockItemDisplay>> GetAll();
    }
}