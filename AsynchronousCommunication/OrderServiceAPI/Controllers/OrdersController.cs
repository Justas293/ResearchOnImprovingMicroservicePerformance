using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace OrderServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly HttpClient m_client;
        private readonly IConnection m_connection;

        public OrdersController(HttpClient client, IConnection connection)
        {
            m_client = client;
            m_client.DefaultRequestHeaders.Accept.Clear();
            m_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            m_connection = connection;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult("OrdersService is up!");
        }

        private void SendMessageToQueueAsync(string basketId)
        {
            var senderId = "getBasketPriceMsgQ";

            using (var channel = m_connection.CreateModel())
            {
                channel.QueueDeclare(queue: senderId, durable: false, exclusive: false, autoDelete: false, arguments: null);

                string message = JsonConvert.SerializeObject(basketId);

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
                    Console.WriteLine("Message sent");
                };
                channel.ConfirmSelect();
            }
        }

        [HttpPost]
        [Route("make/{id}")]
        public async Task<ActionResult<decimal>> PostAsync(string id)
        {
            // Code for simple http request
            /*
            var response = await m_client.GetAsync($"api/basket/price/{id}");
            var price = JsonConvert.DeserializeObject<decimal>(await response.Content.ReadAsStringAsync());

            return new OkObjectResult(price);
            */

            // Code using message queue broker
            decimal totalPrice = 0;
            SendMessageToQueueAsync(id);

            bool noAck = false;
            BasicGetResult result = null;
            using (var channel = m_connection.CreateModel())
            {
                while (result == null)
                {
                    result = channel.BasicGet("getBasketPriceMsgQ_result", noAck);
                    await Task.Delay(1);
                }

                IBasicProperties props = result.BasicProperties;
                byte[] body = result.Body;
                var message = Encoding.UTF8.GetString(body);

                channel.BasicAck(result.DeliveryTag, false);

                totalPrice = JsonConvert.DeserializeObject<decimal>(message);

                return new OkObjectResult(totalPrice);
            }
        }
    }
}
