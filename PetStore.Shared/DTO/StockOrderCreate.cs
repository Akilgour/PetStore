using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.Shared.DTO
{
    public class StockOrderCreate
    {
        public string OrderNumber { get; set; }
        public List<OrderItemsCreate> OrderItems { get; set; }
    }
}
