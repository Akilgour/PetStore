using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PetStore.Blazor.WASM.Shared.Models
{
    public class StockItemUpdate : IValidatableObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int WeightInKg { get; set; }
        public int Quantity { get; set; }
        public decimal CostInPounds { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            return errors;
        }
    }
}
