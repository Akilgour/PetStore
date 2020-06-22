using PetStore.Domain.Models;
using PetStore.Shared.Helpers;
using PetStore.Shared.RabbitMQ;
using PetStore.StockDelivery.Client.Client.Interface;
using System.Threading.Tasks;

namespace PetStore.StockDelivery.Client.Client
{
    public class StockDeliveryClient : BaseSendClient, IStockDeliveryClient
    {
        private const string _requestQueueName = "StockDelivery_Que";
        private const string _exchangeName = ""; // default exchange

        public StockDeliveryClient(RabbitMQConfig rabbitMQConfig)
            : base(rabbitMQConfig, _requestQueueName, _exchangeName)
        {
        }

        public async Task Send(StockItem stockItem)
        {
            await SendAsync(stockItem.Serialize());
        }
    }
}