using PetStore.Blazor.WASM.Shared.Models;

namespace PetStore.Blazor.WASM.Client.Helpers
{
    public class StockOrderCreateCalculateTotalCostInPounds
    {
        public static void Resolve(StockOrderCreate stockOrderCreate)
        {
            decimal result = 0;

            foreach (var item in stockOrderCreate.OrderItems)
            {
                result += item.TotalCostInPounds;
            }

            stockOrderCreate.TotalPrice = result;
        }
    }
}
