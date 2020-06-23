﻿using PetStore.Shared.DTO;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Server.Manager.Interface
{
    public interface IStockDeliveryManager
    {
        Task AddStock(StockDeliveryCreate stockDeliveryCreate);
    }
}