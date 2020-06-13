namespace PetStore.Domain.Models
{
    public class StockItem : BaseModel
    {
        public string Name { get; set; }
        public int WeightInKg { get; set; }
        public int Quantity { get; set; }
    }
}
