using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using BasketServiceAPI.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace BasketServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketDataAccessLayer m_basketDataAccessLayer;
        private readonly HttpClient m_client;
        private readonly IConnection m_connection;

        public BasketController(HttpClient client, IBasketDataAccessLayer basketDataAccessLayer, IConnection connection)
        {
            m_basketDataAccessLayer = basketDataAccessLayer;

            m_client = client;
            m_client.DefaultRequestHeaders.Accept.Clear();
            m_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            m_connection = connection;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult("BasketService is up!");
        }

        private void SendMessageToQueueAsync()
        {
            var senderId = "getProductPricesMsgQ";

            using (var channel = m_connection.CreateModel())
            {
                channel.QueueDeclare(queue: senderId, durable: false, exclusive: false, autoDelete: false, arguments: null);
                var items = m_basketDataAccessLayer.GetBasketItems("123");
                var productIds = items.Select(i => i.ProductId);

                string message = JsonConvert.SerializeObject(productIds);

                var body = Encoding.UTF8.GetBytes(message);

                IBasicProperties properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                properties.DeliveryMode = 2;
                properties.Headers = new Dictionary<string, object>();
                properties.Headers.Add("senderUniqueId", senderId);

                channel.BasicPublish(exchange: "",
                                     routingKey: senderId,
                                     basicProperties: properties,
                                     body: body);

                channel.ConfirmSelect();
                channel.WaitForConfirmsOrDie();

                channel.BasicAcks += (sender, eventArgs) =>
                {
                    Console.WriteLine("Sent RabbitMQ");
                };
                channel.ConfirmSelect();
            }
        }

        [HttpGet("{id}")]
        [Route("price/{id}")]
        public IActionResult GetAsync(string id)
        {
            SendMessageToQueueAsync();

            return Ok();
        }
    }
}
