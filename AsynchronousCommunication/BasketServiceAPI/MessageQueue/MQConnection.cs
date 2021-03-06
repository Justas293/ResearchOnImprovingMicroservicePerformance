﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.IO;
using System.Threading;

namespace BasketServiceAPI.MessageQueue
{
    public class MQConnection : IMQConnection
    {
        private readonly IConnectionFactory _connectionFactory;
        EventBusMQ _eventBusRabbitMQ;
        IConnection _connection;
        bool _disposed;

        public MQConnection(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            if (!IsConnected)
            {
                TryConnect();
            }
        }

        public void CreateConsumerChannel()
        {
            if (!IsConnected)
            {
                TryConnect();
            }

            //Channels
            _eventBusRabbitMQ = new EventBusMQ(this, "getBasketPriceMsgQ");
            _eventBusRabbitMQ.CreateConsumerChannel();
            _eventBusRabbitMQ = new EventBusMQ(this, "getProductPricesMsgQ_result");
            _eventBusRabbitMQ.CreateConsumerChannel();
        }

        public void Disconnect()
        {
            if (_disposed)
            {
                return;
            }
            Dispose();
        }


        public bool IsConnected
        {
            get
            {
                return _connection != null && _connection.IsOpen && !_disposed;
            }
        }

        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
            }
            return _connection.CreateModel();
        }

        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;

            try
            {
                _connection.Dispose();
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public bool TryConnect()
        {
            try
            {
                Console.WriteLine("RabbitMQ Client is trying to connect");
                _connection = _connectionFactory.CreateConnection();
            }
            catch (BrokerUnreachableException)
            {
                Thread.Sleep(5000);
                Console.WriteLine("RabbitMQ Client is trying to reconnect");
                _connection = _connectionFactory.CreateConnection();
            }

            if (IsConnected)
            {
                _connection.ConnectionShutdown += OnConnectionShutdown;
                _connection.CallbackException += OnCallbackException;
                _connection.ConnectionBlocked += OnConnectionBlocked;

                Console.WriteLine($"RabbitMQ persistent connection acquired a connection {_connection.Endpoint.HostName} and is subscribed to failure events");

                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            if (_disposed) return;
            Console.WriteLine("A RabbitMQ connection is shutdown. Trying to re-connect...");
            TryConnect();
        }

        void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            if (_disposed) return;
            Console.WriteLine("A RabbitMQ connection throw exception. Trying to re-connect...");
            TryConnect();
        }

        void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
        {
            if (_disposed) return;
            Console.WriteLine("A RabbitMQ connection is on shutdown. Trying to re-connect...");
            TryConnect();
        }
    }
}
