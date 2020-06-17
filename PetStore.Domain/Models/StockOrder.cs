using System.Collections.Generic;

namespace PetStore.Domain.Models
{
    public class StockOrder : BaseModel
    {
        public string OrderNumber { get; set; }
        public List<StockItem> OrderItems { get; set; }
    }
}
