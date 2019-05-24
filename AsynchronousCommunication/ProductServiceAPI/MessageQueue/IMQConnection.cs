using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductServiceAPI.MessageQueue
{
    public interface IMQConnection
    {
        bool IsConnected { get; }
        bool TryConnect();
        IModel CreateModel();
        void CreateConsumerChannel();
        void Disconnect();
    }
}
