using System;
using System.ComponentModel.DataAnnotations;

namespace PetStore.Blazor.WASM.Server.Models
{
    public class StockDeliveryNew
    {
        [Required( ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100.")]
        public int? Quantity { get; set; }
    }
}