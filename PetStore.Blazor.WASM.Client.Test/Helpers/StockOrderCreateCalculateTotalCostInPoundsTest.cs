using PetStore.Blazor.WASM.Client.Helpers;
using PetStore.Blazor.WASM.Shared.Models;
using Xunit;

namespace PetStore.Blazor.WASM.Client.Test.Helpers
{
    public class StockOrderCreateCalculateTotalCostInPoundsTest
    {
        [Fact]
        public void Resovle_WhenListEmpty()
        {
            //arrange
            var stockOrderCreate = new StockOrderCreate();
            //act
            StockOrderCreateCalculateTotalCostInPounds.Resolve(stockOrderCreate);
            //assert
            Assert.Equal(0, stockOrderCreate.TotalPrice);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(10, 11)]
        [InlineData(5.5, 6.5)]
        [InlineData(1.2, 2.2)]
        public void Resolve(decimal totalCostInPounds, decimal expected)
        {
            //arrange
            var stockOrderCreate = new StockOrderCreate();
            stockOrderCreate.OrderItems.Add(new OrderItemsCreate() { TotalCostInPounds = 1 });
            stockOrderCreate.OrderItems.Add(new OrderItemsCreate() { TotalCostInPounds = totalCostInPounds });
            //act
            StockOrderCreateCalculateTotalCostInPounds.Resolve(stockOrderCreate);
            //assert
            Assert.Equal(expected, stockOrderCreate.TotalPrice);
        }
    }
}
