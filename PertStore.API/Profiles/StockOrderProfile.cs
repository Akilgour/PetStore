using AutoMapper;
using PetStore.Domain.Models;
using PetStore.Shared.DTO;

namespace PetStore.API.Profiles
{
    public class StockOrderProfile : Profile
    {
        public StockOrderProfile()
        {
            CreateMap<StockOrderCreate, StockOrder>();
            CreateMap<OrderItemsCreate, StockItem>();
        }
    }
}