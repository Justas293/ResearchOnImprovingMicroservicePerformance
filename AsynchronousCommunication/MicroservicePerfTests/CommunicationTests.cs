using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using ProductServiceAPI.Models;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace MicroservicePerfTests
{
    [TestClass]
    public class CommunicationTests
    {
        private readonly HttpClient m_httpClient;
        private readonly string m_baseAddress;

        public CommunicationTests()
        {
            m_httpClient = new HttpClient();
            m_httpClient.DefaultRequestHeaders.Accept.Clear();
            m_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            m_baseAddress = "http://localhost:63315/";
        }

        [TestMethod]
        public async Task TestMicroserviceCommunication()
        {
            var sw = new Stopwatch();
            long total = 0;
            decimal price = 0;

            sw.Start();
            for (int i = 0; i < 1000; i++)
            {
                sw.Restart();
                var response = await m_httpClient.PostAsync($"{m_baseAddress}api/orders/make/123", null);
                price = JsonConvert.DeserializeObject<decimal>(await response.Content.ReadAsStringAsync());
                total += sw.ElapsedMilliseconds;
            }
            Console.WriteLine($"Total time elapsed: {total}");
            Console.WriteLine($"Average for 1 operation: {total / 1000}");
        }
    }
}
