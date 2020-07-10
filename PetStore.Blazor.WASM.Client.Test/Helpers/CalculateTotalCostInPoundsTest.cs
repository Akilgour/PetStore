using PetStore.Blazor.WASM.Client.Helpers;
using PetStore.Blazor.WASM.Shared.Models;
using Xunit;

namespace PetStore.Blazor.WASM.Client.Test.Helpers
{
    public class CalculateTotalCostInPoundsTest
    {
        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(1, 10, 10)]
        [InlineData(5, 5.5, 27.5)]
        [InlineData(7, 1.2, 8.4)]
        public void Resolve(int quantity, decimal price, decimal expected)
        {
            //arrange
            var orderItemsCreate = new OrderItemsCreate() { Quantity = quantity, CostInPounds = price };
            //act
            CalculateTotalCostInPounds.Resolve(orderItemsCreate);
            //assert
            Assert.Equal(expected, orderItemsCreate.TotalCostInPounds);
        }
    }
}