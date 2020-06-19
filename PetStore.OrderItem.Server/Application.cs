using PetStore.Domain.Models;
using PetStore.OrderItem.Manager.Manager.Interface;
using PetStore.Shared.Helpers;
using PetStore.Shared.RabbitMQ;
using PetStore.Shared.RabbitMQ.Factorys;
using RabbitMQ.Client.Events;
using System;
using System.Threading.Tasks;

namespace PetStore.OrderItem.Server
{
    public class Application : BaseReceiveReplyServer
    {
        private const string _requestQueueName = "OrderItem_RequestQueue";
        private readonly IOrderItemManager _orderItemManager;
        private const string _exchangeName = "";

        public Application(IOrderItemManager orderItemManager)
           : base(RabbitMQConfigFactory.Create(), _requestQueueName, _exchangeName)
        {
            _orderItemManager = orderItemManager;
        }

        public void Run()
        {
            WaitForResponse();
        }

        protected override async Task Receive(object sender, BasicDeliverEventArgs e)
        {
            var stockOrder = (StockOrder)e.Body.ToArray().DeSerialize(typeof(StockOrder));
            var correlationId = e.BasicProperties.CorrelationId;
            string responseQueueName = e.BasicProperties.ReplyTo;

            Console.WriteLine($"Received: {stockOrder.OrderNumber} with CorrelationId {correlationId}");
            var responseMessage = await _orderItemManager.Order(stockOrder);
            Reply( correlationId, responseQueueName, responseMessage.Serialize());
        }
    }
}