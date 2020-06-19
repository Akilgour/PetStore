﻿using PetStore.Shared.Helpers;
using PetStore.Shared.QueMessages;
using PetStore.Shared.RabbitMQ;
using PetStore.Shared.RabbitMQ.Factorys;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace PetStore.OrderItem.Client
{
    public class RpcClient : BaseSendReceiveClient
    {
        private readonly ConcurrentDictionary<string, TaskCompletionSource<OrderResponse>> _pendingMessages;
        private const string _requestQueueName = "OrderItem_RequestQueue";
        private const string _responseQueueName = "OrderItem_ResponseQueue";
        private const string _exchangeName = ""; // default exchange

        public RpcClient()
            : base(RabbitMQConfigFactory.Create(), _requestQueueName, _responseQueueName, _exchangeName)
        {
            Send();
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

        protected override void Receive(object sender, BasicDeliverEventArgs e)
        {
            var correlationId = e.BasicProperties.CorrelationId;
            var orderResponse = (OrderResponse)e.Body.ToArray().DeSerialize(typeof(OrderResponse));
            this._pendingMessages.TryRemove(correlationId, out var tcs);
            if (tcs != null)
            {
                tcs.SetResult(orderResponse);
            }
        }
    }
}