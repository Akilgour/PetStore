using PetStore.Shared.Helpers;
using PetStore.Shared.QueMessages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace PetStore.OrderItem.Client
{
    public class RpcClient : IDisposable
    {
        private bool _disposed = false;
        private IConnection _connection;
        private IModel _channel;
        private EventingBasicConsumer _consumer;
        private ConcurrentDictionary<string, TaskCompletionSource<OrderResponse>> _pendingMessages;

        private const string requestQueueName = "OrderItem_RequestQueue";
        private const string responseQueueName = "OrderItem_ResponseQueue";
        private const string exchangeName = ""; // default exchange

        public RpcClient()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(requestQueueName, true, false, false, null);
            _channel.QueueDeclare(responseQueueName, true, false, false, null);

            _consumer = new EventingBasicConsumer(this._channel);
            _consumer.Received += Consumer_Received;
            _channel.BasicConsume(responseQueueName, true, _consumer);

            _pendingMessages = new ConcurrentDictionary<string, TaskCompletionSource<OrderResponse>>();
        }

        public Task<OrderResponse> SendAsync(byte[] message)
        {
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
            props.ReplyTo = responseQueueName;
            _channel.BasicPublish(exchangeName, requestQueueName, props, message);
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var correlationId = e.BasicProperties.CorrelationId;
            var orderResponse = (OrderResponse)e.Body.ToArray().DeSerialize(typeof(OrderResponse));
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
            if (!_disposed && disposing)
            {
                _channel?.Dispose();
                _connection?.Dispose();
            }
            _disposed = true;
        }
    }
}