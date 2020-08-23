using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PetStore.Blazor.WASM.Shared.Models
{
    public class StockItemCreateCommand
    {
        [Required]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Weight must be greater than 0.")]
        public int WeightInKg { get; set; }

        [Required]
        [Range(1, 50, ErrorMessage = "Quantity must be between 1 and 50.")]
        public int Quantity { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Cost must be greater than 0.")]
        public decimal CostInPounds { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            return errors;
        }
    }
}