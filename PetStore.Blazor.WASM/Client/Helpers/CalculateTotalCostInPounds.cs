using PetStore.Blazor.WASM.Shared.Models;

namespace PetStore.Blazor.WASM.Client.Helpers
{
    public class CalculateTotalCostInPounds
    {
        public static void Resolve(OrderItemsCreate orderItemsCreate)
        {
            orderItemsCreate.TotalCostInPounds = (orderItemsCreate.CostInPounds * orderItemsCreate.Quantity).Value;
        }
    }
}