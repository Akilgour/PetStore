using PetStore.Domain.Models;
using PetStore.Shared.Helpers;
using PetStore.Shared.RabbitMQ;

namespace PetStore.StockDelivery.Client.Client
{
    public class StockDeliveryClient : BaseSendClient
    {
        private const string _requestQueueName = "StockDelivery_Que";
        private const string _exchangeName = ""; // default exchange

        public StockDeliveryClient(RabbitMQConfig rabbitMQConfig)
            : base(rabbitMQConfig, _requestQueueName, _exchangeName)
        {
        }

        public void Send(StockItem stockItem)
        {
            Send(stockItem.Serialize());
        }
    }
}