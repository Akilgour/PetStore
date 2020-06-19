using System.ComponentModel.DataAnnotations;

namespace PetStore.Shared.DTO
{
    public class StockDeliveryCreate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}