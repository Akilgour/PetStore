using Moq;
using PetStore.Data.Repositorys.Interface;
using PetStore.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PetStore.OrderItem.Manger.Manger.Test.Manger
{
    public class OrderItemMangerTest
    {
        [Fact]
        public async Task OrderOkay()
        {
            //arrange
            var repository = new Mock<IStockItemRepository>();
            repository.Setup(x => x.GetByName("AA")).Returns(Task.FromResult(new StockItem() { Name = "AA", Quantity = 10 }));
            repository.Setup(x => x.GetByName("BB")).Returns(Task.FromResult(new StockItem() { Name = "BB", Quantity = 10 }));
            repository.Setup(x => x.GetByName("CC")).Returns(Task.FromResult(new StockItem() { Name = "CC", Quantity = 10 }));

            var orderItemManger = new OrderItemManger(repository.Object);

            var stockOrder = new StockOrder()
            {
                OrderNumber = "ABC 123",
                OrderItems = new List<StockItem>()
                {
                    new StockItem() { Name = "AA", Quantity = 2 },
                    new StockItem() { Name = "BB", Quantity = 3 },
                    new StockItem() { Name = "CC", Quantity = 4 }
                }
            };

            //act
            var value = await orderItemManger.Order(stockOrder);

            //assert
            Assert.Equal("Success Ordered ABC 123", value.Message);
            Assert.True(value.Success);

            repository.Verify(a => a.Update(It.Is<StockItem>(s => s.Name == "AA" && s.Quantity == 8)), Times.Once);
            repository.Verify(a => a.Update(It.Is<StockItem>(s => s.Name == "BB" && s.Quantity == 7)), Times.Once);
            repository.Verify(a => a.Update(It.Is<StockItem>(s => s.Name == "CC" && s.Quantity == 6)), Times.Once);
            repository.Verify(x => x.Update(It.IsAny<StockItem>()), Times.Exactly(3));
        }

        [Fact]
        public async Task OrderOkay_OrderAllStock()
        {
            //arrange
            var repository = new Mock<IStockItemRepository>();
            repository.Setup(x => x.GetByName("AA")).Returns(Task.FromResult(new StockItem() { Name = "AA", Quantity = 10 }));
            repository.Setup(x => x.GetByName("BB")).Returns(Task.FromResult(new StockItem() { Name = "BB", Quantity = 10 }));
            repository.Setup(x => x.GetByName("CC")).Returns(Task.FromResult(new StockItem() { Name = "CC", Quantity = 10 }));

            var orderItemManger = new OrderItemManger(repository.Object);

            var stockOrder = new StockOrder()
            {
                OrderNumber = "ZXY 789",
                OrderItems = new List<StockItem>()
                {
                    new StockItem() { Name = "AA", Quantity = 10 },
                    new StockItem() { Name = "BB", Quantity = 10 },
                    new StockItem() { Name = "CC", Quantity = 10 }
                }
            };

            //act
            var value = await orderItemManger.Order(stockOrder);

            //assert
            Assert.Equal("Success Ordered ZXY 789", value.Message);
            Assert.True(value.Success);

            repository.Verify(a => a.Update(It.Is<StockItem>(s => s.Name == "AA" && s.Quantity == 0)), Times.Once);
            repository.Verify(a => a.Update(It.Is<StockItem>(s => s.Name == "BB" && s.Quantity == 0)), Times.Once);
            repository.Verify(a => a.Update(It.Is<StockItem>(s => s.Name == "CC" && s.Quantity == 0)), Times.Once);
            repository.Verify(x => x.Update(It.IsAny<StockItem>()), Times.Exactly(3));
        }

        [Fact]
        public async Task Order_TwoItems_NotEnoughtStock()
        {
            //arrange
            var repository = new Mock<IStockItemRepository>();
            repository.Setup(x => x.GetByName("AA")).Returns(Task.FromResult(new StockItem() { Name = "AA", Quantity = 10 }));
            repository.Setup(x => x.GetByName("BB")).Returns(Task.FromResult(new StockItem() { Name = "BB", Quantity = 10 }));
            repository.Setup(x => x.GetByName("CC")).Returns(Task.FromResult(new StockItem() { Name = "CC", Quantity = 10 }));

            var orderItemManger = new OrderItemManger(repository.Object);

            var stockOrder = new StockOrder()
            {
                OrderNumber = "ABC 123",
                OrderItems = new List<StockItem>()
                {
                    new StockItem() { Name = "AA", Quantity = 20 },
                    new StockItem() { Name = "BB", Quantity = 3 },
                    new StockItem() { Name = "CC", Quantity = 40 }
                }
            };

            //act
            var value = await orderItemManger.Order(stockOrder);

            //assert
            Assert.Equal("Order ABC 123, can not be placed not enought stock AA, CC.", value.Message);
            Assert.False(value.Success);
            repository.Verify(x => x.Update(It.IsAny<StockItem>()), Times.Never());
        }

        [Fact]
        public async Task Order_OneItem_NotEnoughtStock()
        {
            //arrange
            var repository = new Mock<IStockItemRepository>();
            repository.Setup(x => x.GetByName("AA")).Returns(Task.FromResult(new StockItem() { Name = "AA", Quantity = 10 }));
            repository.Setup(x => x.GetByName("BB")).Returns(Task.FromResult(new StockItem() { Name = "BB", Quantity = 10 }));
            repository.Setup(x => x.GetByName("CC")).Returns(Task.FromResult(new StockItem() { Name = "CC", Quantity = 10 }));

            var orderItemManger = new OrderItemManger(repository.Object);

            var stockOrder = new StockOrder()
            {
                OrderNumber = "ABC 345",
                OrderItems = new List<StockItem>()
                {
                    new StockItem() { Name = "AA", Quantity = 2 },
                    new StockItem() { Name = "BB", Quantity = 30 },
                    new StockItem() { Name = "CC", Quantity = 4 }
                }
            };

            //act
            var value = await orderItemManger.Order(stockOrder);

            //assert
            Assert.Equal("Order ABC 345, can not be placed not enought stock BB.", value.Message);
            Assert.False(value.Success);
            repository.Verify(x => x.Update(It.IsAny<StockItem>()), Times.Never());
        }
    }
}