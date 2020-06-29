using PetStore.Domain.Models;
using PetStore.OrderItem.Client.Client.Interface;
using PetStore.Shared.Helpers;
using PetStore.Shared.QueMessages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace PetStore.OrderItem.Client
{
    public class OrderItemClient : IDisposable, IOrderItemClient
    {
    //    https://gigi.nullneuron.net/gigilabs/tag/taskcompletionsource/


       private  ConcurrentDictionary<string, TaskCompletionSource<OrderResponse>> _pendingMessages;
        private const string _requestQueueName = "OrderItem_RequestQueue";
        private const string _responseQueueName = "OrderItem_ResponseQueue";
        private const string _exchangeName = ""; // default exchange
        private EventingBasicConsumer _consumer;
        private IConnection _connection;
        private IModel _channel;

        private bool disposed = false;

        public OrderItemClient()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(_requestQueueName, true, false, false, null);
            _channel.QueueDeclare(_responseQueueName, true, false, false, null);
            _consumer = new EventingBasicConsumer(this._channel);
            _consumer.Received += Receive;
            _channel.BasicConsume(_responseQueueName, true, _consumer);
            var replyQueueName = _channel.QueueDeclare().QueueName;
        }

        public Task<OrderResponse> Send(StockOrder stockOrder)
        {
            _pendingMessages = new ConcurrentDictionary<string, TaskCompletionSource<OrderResponse>>();
            var message = stockOrder.Serialize();
            var tcs = new TaskCompletionSource<OrderResponse>();
            var correlationId = Guid.NewGuid().ToString();
            _pendingMessages[correlationId] = tcs;
            Publish(message, correlationId);
            return tcs.Task;
        }

        private void Publish(byte[] message, string correlationId)
        {
            var props = this._channel.CreateBasicProperties();
            props.CorrelationId = correlationId;
            props.ReplyTo = _responseQueueName;
            this._channel.BasicPublish(_exchangeName, _requestQueueName, props, message);
        }

        protected void Receive(object sender, BasicDeliverEventArgs e)
        {
            var correlationId = e.BasicProperties.CorrelationId;
            var orderResponse = (OrderResponse)e.Body.ToArray().DeSerialize(typeof(OrderResponse));

            //On the first time round _pendingMessages has one item in it, and is the item we are looking for
            //On the second call it is empty
            this._pendingMessages.TryRemove(correlationId, out var tcs);
            if (tcs != null)
            {
                tcs.SetResult(orderResponse);
            }
        }



        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                _channel?.Dispose();
                _connection?.Dispose();
            }

            this.disposed = true;
        }
    }

}