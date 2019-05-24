using BasketServiceAPI.DataAccess;
using BasketServiceAPI.Models;
using BasketServiceAPI.Services;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasketServiceAPI.MessageQueue
{
    public class EventBusMQ : IDisposable
    {
        private readonly IMQConnection m_persistentConnection;
        private IModel m_consumerChannel;
        private string m_queueName;
        private BasketService m_basketService;

        public EventBusMQ(IMQConnection connection, string queueName = null)
        {
            m_persistentConnection = connection ?? throw new ArgumentNullException(nameof(connection));
            m_queueName = queueName;
            m_basketService = new BasketService(new BasketDataAccessLayer());
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

            //Create event when something receive
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
            if (e.RoutingKey == "getBasketPriceMsgQ")
            {
                var message = Encoding.UTF8.GetString(e.Body);
                string baskedId = JsonConvert.DeserializeObject<string>(message);
                IEnumerable<Item> items = m_basketService.GetBasketItems(baskedId);
                var productIds = items.Select(i => i.ProductId);

                PublishProductIdsForPrices("getProductPricesMsgQ", productIds, e.BasicProperties.Headers);
            }
            else if (e.RoutingKey == "getProductPricesMsgQ_result")
            {
                var message = Encoding.UTF8.GetString(e.Body);
                IEnumerable<decimal> productPrices = JsonConvert.DeserializeObject<IEnumerable<decimal>>(message);
                decimal totalPrice = 0;

                foreach(var price in productPrices)
                {
                    totalPrice += price;
                }

                PublishBasketPrice("getBasketPriceMsgQ_result", totalPrice, e.BasicProperties.Headers);
            }
        }

        public void PublishProductIdsForPrices(string queueName, IEnumerable<string> ids, IDictionary<string, object> headers)
        {

            if (!m_persistentConnection.IsConnected)
            {
                m_persistentConnection.TryConnect();
            }

            using (var channel = m_persistentConnection.CreateModel())
            {

                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                var message = JsonConvert.SerializeObject(ids);
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

        public void PublishBasketPrice(string queueName, decimal price, IDictionary<string, object> headers)
        {

            if (!m_persistentConnection.IsConnected)
            {
                m_persistentConnection.TryConnect();
            }

            using (var channel = m_persistentConnection.CreateModel())
            {

                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                var message = JsonConvert.SerializeObject(price);
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
