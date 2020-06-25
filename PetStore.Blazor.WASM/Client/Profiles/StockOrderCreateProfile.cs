using AutoMapper;
using PetStore.Blazor.WASM.Shared.Models;
using PetStore.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Client.Profiles
{
    public class StockOrderCreateProfile : Profile
    {
        public StockOrderCreateProfile()
        {
            CreateMap<StockOrderNew, StockOrderCreate>();
            CreateMap<OrderItemsNew, OrderItemsCreate>();
        }
    }
}