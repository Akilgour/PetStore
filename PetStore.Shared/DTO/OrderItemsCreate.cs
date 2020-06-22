using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.Shared.DTO
{
    public class OrderItemsCreate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
