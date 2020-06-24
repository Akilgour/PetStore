using System;
using System.ComponentModel.DataAnnotations;

namespace PetStore.Blazor.WASM.Server.Models
{
    public class StockItemsNew
    {
        [Required]
        public string Name { get; set; }

        [Range(0, 100, ErrorMessage = "Quantity must be between 0 and 100.")]
        public int Quantity { get; set; }
    }
}