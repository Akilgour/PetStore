﻿using System;
using System.ComponentModel.DataAnnotations;

namespace PetStore.Blazor.WASM.Shared.Models
{
    public class OrderItemsCreate
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, 50, ErrorMessage = "Quantity must be between 1 and 50.")]
        public int? Quantity { get; set; }

        public decimal CostInPounds { get; set; }
        public decimal TotalCostInPounds { get; set; }
    }
}