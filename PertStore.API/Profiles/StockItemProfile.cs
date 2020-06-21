using AutoMapper;
using PetStore.Domain.Models;
using PetStore.Shared.DTO;

namespace PetStore.API.Profiles
{
    public class StockItemProfile : Profile
    {
        public StockItemProfile()
        {
            CreateMap<StockDeliveryCreate, StockItem>();
        }
    }
}