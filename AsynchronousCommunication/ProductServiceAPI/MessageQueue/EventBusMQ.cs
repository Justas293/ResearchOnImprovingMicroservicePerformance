using Newtonsoft.Json;
using ProductServiceAPI.DataAccess;
using ProductServiceAPI.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductServiceAPI.MessageQueue
{
    public class EventBusMQ : IDisposable
    {
        private readonly IMQConnection m_persistentConnection;
        private IModel m_consumerChannel;
        private string m_queueName;
        private ProductService m_productService;

        public EventBusMQ(IMQConnection connection, string queueName = null)
        {
            m_persistentConnection = connection ?? throw new ArgumentNullException(nameof(connection));
            m_queueName = queueName;
            m_productService = new ProductService(new ProductDataAccessLayer());
        }

        public IModel CreateConsumerChannel()
        {
            if (!m_persistentConnection.IsConnected)
            {
                m_persistentConnection.TryConnect();
            }

            var channel = m_persistentConnection.CreateModel();
            channel.QueueDeclare(queue: m_queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += ReceivedEvent;

            channel.BasicConsume(queue: m_queueName, autoAck: true, consumer: consumer);
            channel.CallbackException += (sender, ea) =>
            {
                m_consumerChannel.Dispose();
                m_consumerChannel = CreateConsumerChannel();
            };
            return channel;
        }

        private void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            if (e.RoutingKey == "getProductPricesMsgQ")
            {
                var message = Encoding.UTF8.GetString(e.Body);
                IEnumerable<string> productIds = JsonConvert.DeserializeObject<IEnumerable<string>>(message);
                IEnumerable<decimal> prices = m_productService.GetProductPrices(productIds);

                PublishProductPrices("getProductPricesMsgQ_result", prices, e.BasicProperties.Headers);
            }
        }

        public void PublishProductPrices(string queueName, IEnumerable<decimal> prices, IDictionary<string, object> headers)
        {

            if (!m_persistentConnection.IsConnected)
            {
                m_persistentConnection.TryConnect();
            }

            using (var channel = m_persistentConnection.CreateModel())
            {

                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                var message = JsonConvert.SerializeObject(prices);
                var body = Encoding.UTF8.GetBytes(message);

                IBasicProperties properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                properties.DeliveryMode = 2;
                properties.Headers = headers;

                channel.ConfirmSelect();
                channel.BasicPublish(exchange: "", routingKey: queueName, mandatory: true, basicProperties: properties, body: body);
                channel.WaitForConfirmsOrDie();

                channel.BasicAcks += (sender, eventArgs) =>
                {
                    Console.WriteLine("Message sent");
                };
                channel.ConfirmSelect();
            }
        }

        public void Dispose()
        {
            if (m_consumerChannel != null)
            {
                m_consumerChannel.Dispose();
            }
        }
    }
}
