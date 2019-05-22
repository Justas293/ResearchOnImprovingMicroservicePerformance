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
using System.Linq;

namespace MicroservicePerfTests
{
    [TestClass]
    public class ParallelProgrammingTests
    {
        private readonly HttpClient m_httpClient;
        private readonly string m_baseAddress;
        private List<Product> m_products;
        private List<StringContent> m_contents;

        public ParallelProgrammingTests()
        {
            m_httpClient = new HttpClient();
            m_httpClient.DefaultRequestHeaders.Accept.Clear();
            m_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            m_baseAddress = "http://localhost:5001/";
        }

        private async Task Initialize(int entryCount)
        {
            m_contents = new List<StringContent>();
            m_products = new List<Product>();
            for (int i = 0; i < entryCount; i++)
            {
                var product = new Product();
                product.Id = Guid.NewGuid().ToString();
                product.Name = "Test";
                product.Description = "Test";
                product.Price = 10;
                product.CategoryId = 1;
                m_products.Add(product);

                var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                m_contents.Add(content);
            }

            foreach (var content in m_contents)
            {
                var response = await m_httpClient.PostAsync($"{m_baseAddress}api/product", content);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            using (var con = new SqlConnection("Server=tcp:justassqlserver.database.windows.net,1433;Initial Catalog=ProductDb;Persist Security Info=False;User ID=justas293;Password=;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                con.Open();
                var cmd = new SqlCommand("TRUNCATE TABLE Products", con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        [TestMethod]
        public async Task Test1000GetSynchronously()
        {
            await Initialize(1000);
            var sw = new Stopwatch();
            long totalTimeNeeded = 0;
            List<Product> products = new List<Product>();

            sw.Start();
            foreach (var p in m_products)
            {
                sw.Restart();
                products.Add(await GetProduct(p.Id));
                totalTimeNeeded += sw.ElapsedMilliseconds;
            }

            Console.WriteLine($"Total time needed: {totalTimeNeeded}");
            Console.WriteLine($"Average time needed for 1 get: {totalTimeNeeded / m_products.Count}");
        }

        [TestMethod]
        public async Task Test10000GetSynchronously()
        {
            await Initialize(10000);
            var sw = new Stopwatch();
            long totalTimeNeeded = 0;
            List<Product> products = new List<Product>();

            sw.Start();
            foreach (var p in m_products)
            {
                sw.Restart();
                products.Add(await GetProduct(p.Id));
                totalTimeNeeded += sw.ElapsedMilliseconds;
            }

            Console.WriteLine($"Total time needed: {totalTimeNeeded}");
            Console.WriteLine($"Average time needed for 1 get: {totalTimeNeeded / m_products.Count}");
        }

        [TestMethod]
        public async Task Test1000GetInParallel()
        {
            await Initialize(1000);
            var sw = new Stopwatch();
            long total = 0;

            var tasks = m_products.Select(p => GetProduct(p.Id));
            sw.Start();
            var products = await Task.WhenAll(tasks);
            total = sw.ElapsedMilliseconds;
            Console.WriteLine($"Total time needed: {total}");
            Console.WriteLine($"Average for 1: {total / m_products.Count}");
        }

        [TestMethod]
        public async Task Test10000GetInParallel()
        {
            await Initialize(10000);
            var sw = new Stopwatch();
            long total = 0;

            var tasks = m_products.Select(p => GetProduct(p.Id));
            sw.Start();
            var products = await Task.WhenAll(tasks);
            total = sw.ElapsedMilliseconds;
            Console.WriteLine($"Total time needed: {total}");
            Console.WriteLine($"Average for 1: {total / m_products.Count}");
        }

        [TestMethod]
        public async Task Test1000GetInBatchesOf20()
        {
            await Initialize(1000);
            var sw = new Stopwatch();
            long total = 0;
            var products = new List<Product>();
            var batchSize = 20;

            sw.Start();
            for (int i = 0; i < 50; i++)
            {
                var ids = m_products.Select(p => p.Id).Skip(i * batchSize).Take(batchSize);
                var tasks = ids.Select(id => GetProduct(id));
                products.AddRange(await Task.WhenAll(tasks));
            }
            total = sw.ElapsedMilliseconds;
            Console.WriteLine($"Total time needed: {total}");
            Console.WriteLine($"Average for 1: {total / m_products.Count}");
        }

        [TestMethod]
        public async Task Test1000GetInBatchesOf50()
        {
            await Initialize(1000);
            var sw = new Stopwatch();
            long total = 0;
            var products = new List<Product>();
            var batchSize = 50;

            sw.Start();
            for (int i = 0; i < 20; i++)
            {
                var ids = m_products.Select(p => p.Id).Skip(i * batchSize).Take(batchSize);
                var tasks = ids.Select(id => GetProduct(id));
                products.AddRange(await Task.WhenAll(tasks));
            }
            total = sw.ElapsedMilliseconds;
            Console.WriteLine($"Total time needed: {total}");
            Console.WriteLine($"Average for 1: {total / m_products.Count}");
        }

        [TestMethod]
        public async Task Test10000GetInBatchesOf50()
        {
            var sw = new Stopwatch();
            long total = 0;
            var products = new List<Product>();
            var batchSize = 50;

            sw.Start();
            for (int i = 0; i < 200; i++)
            {
                var ids = m_products.Select(p => p.Id).Skip(i * batchSize).Take(batchSize);
                var tasks = ids.Select(id => GetProduct(id));
                products.AddRange(await Task.WhenAll(tasks));
            }
            total = sw.ElapsedMilliseconds;
            Console.WriteLine($"Total time needed: {total}");
            Console.WriteLine($"Average for 1: {total / m_products.Count}");
        }

        [TestMethod]
        public async Task Test1000GetInBatchesOf100()
        {
            await Initialize(1000);
            var sw = new Stopwatch();
            long total = 0;
            var products = new List<Product>();
            var batchSize = 100;

            sw.Start();
            for (int i = 0; i < 10; i++)
            {
                var ids = m_products.Select(p => p.Id).Skip(i * batchSize).Take(batchSize);
                var tasks = ids.Select(id => GetProduct(id));
                products.AddRange(await Task.WhenAll(tasks));
            }
            total = sw.ElapsedMilliseconds;
            Console.WriteLine($"Total time needed: {total}");
            Console.WriteLine($"Average for 1: {total / m_products.Count}");
        }

        [TestMethod]
        public async Task Test1000GetInBatchesOf200()
        {
            await Initialize(1000);
            var sw = new Stopwatch();
            long total = 0;
            var products = new List<Product>();
            var batchSize = 200;

            sw.Start();
            for (int i = 0; i < 5; i++)
            {
                var ids = m_products.Select(p => p.Id).Skip(i * batchSize).Take(batchSize);
                var tasks = ids.Select(id => GetProduct(id));
                products.AddRange(await Task.WhenAll(tasks));
            }
            total = sw.ElapsedMilliseconds;
            Console.WriteLine($"Total time needed: {total}");
            Console.WriteLine($"Average for 1: {total / m_products.Count}");
        }

        [TestMethod]
        public async Task Test1000GetInBatchesOf500()
        {
            await Initialize(1000);
            var sw = new Stopwatch();
            long total = 0;
            var products = new List<Product>();
            var batchSize = 500;

            sw.Start();
            for (int i = 0; i < 2; i++)
            {
                var ids = m_products.Select(p => p.Id).Skip(i * batchSize).Take(batchSize);
                var tasks = ids.Select(id => GetProduct(id));
                products.AddRange(await Task.WhenAll(tasks));
            }
            total = sw.ElapsedMilliseconds;
            Console.WriteLine($"Total time needed: {total}");
            Console.WriteLine($"Average for 1: {total / m_products.Count}");
        }

        [TestMethod]
        public async Task Test10000GetInBatchesOf100()
        {
            await Initialize(10000);
            var sw = new Stopwatch();
            long total = 0;
            var products = new List<Product>();
            var batchSize = 100;

            sw.Start();
            for (int i = 0; i < 100; i++)
            {
                var ids = m_products.Select(p => p.Id).Skip(i * batchSize).Take(batchSize);
                var tasks = ids.Select(id => GetProduct(id));
                products.AddRange(await Task.WhenAll(tasks));
            }
            total = sw.ElapsedMilliseconds;
            Console.WriteLine($"Total time needed: {total}");
            Console.WriteLine($"Average for 1: {total / m_products.Count}");
        }

        [TestMethod]
        public async Task Test10000GetInBatchesOf200()
        {
            await Initialize(10000);
            var sw = new Stopwatch();
            long total = 0;
            var products = new List<Product>();
            var batchSize = 200;

            sw.Start();
            for (int i = 0; i < 50; i++)
            {
                var ids = m_products.Select(p => p.Id).Skip(i * batchSize).Take(batchSize);
                var tasks = ids.Select(id => GetProduct(id));
                products.AddRange(await Task.WhenAll(tasks));
            }
            total = sw.ElapsedMilliseconds;
            Console.WriteLine($"Total time needed: {total}");
            Console.WriteLine($"Average for 1: {total / m_products.Count}");
        }

        [TestMethod]
        public async Task Test10000GetInBatchesOf500()
        {
            await Initialize(10000);
            var sw = new Stopwatch();
            long total = 0;
            var products = new List<Product>();
            var batchSize = 500;

            sw.Start();
            for (int i = 0; i < 20; i++)
            {
                var ids = m_products.Select(p => p.Id).Skip(i * batchSize).Take(batchSize);
                var tasks = ids.Select(id => GetProduct(id));
                products.AddRange(await Task.WhenAll(tasks));
            }
            total = sw.ElapsedMilliseconds;
            Console.WriteLine($"Total time needed: {total}");
            Console.WriteLine($"Average for 1: {total / m_products.Count}");
        }

        [TestMethod]
        public async Task Test10000GetInBatchesOf1000()
        {
            await Initialize(10000);
            var sw = new Stopwatch();
            long total = 0;
            var products = new List<Product>();
            var batchSize = 1000;

            sw.Start();
            for (int i = 0; i < 10; i++)
            {
                var ids = m_products.Select(p => p.Id).Skip(i * batchSize).Take(batchSize);
                var tasks = ids.Select(id => GetProduct(id));
                products.AddRange(await Task.WhenAll(tasks));
            }
            total = sw.ElapsedMilliseconds;
            Console.WriteLine($"Total time needed: {total}");
            Console.WriteLine($"Average for 1: {total / m_products.Count}");
        }

        [TestMethod]
        public async Task Test10000GetInBatchesOf1250()
        {
            await Initialize(10000);
            var sw = new Stopwatch();
            long total = 0;
            var products = new List<Product>();
            var batchSize = 1250;

            sw.Start();
            for (int i = 0; i < 8; i++)
            {
                var ids = m_products.Select(p => p.Id).Skip(i * batchSize).Take(batchSize);
                var tasks = ids.Select(id => GetProduct(id));
                products.AddRange(await Task.WhenAll(tasks));
            }
            total = sw.ElapsedMilliseconds;
            Console.WriteLine($"Total time needed: {total}");
            Console.WriteLine($"Average for 1: {total / m_products.Count}");
        }

        [TestMethod]
        public async Task Test1000GetUsingApiBatches()
        {
            await Initialize(1000);
            var sw = new Stopwatch();
            long total = 0;
            var batchSize = 100;

            var tasks = new List<Task<IEnumerable<Product>>>();
            for (int i = 0; i < 10; i++)
            {
                var ids = m_products.Select(p => p.Id).Skip(i * batchSize).Take(batchSize);
                tasks.Add(GetProductBatch(ids));
            }
            sw.Start();
            var products = await Task.WhenAll(tasks);
            total = sw.ElapsedMilliseconds;
            Console.WriteLine($"Total time needed: {total}");
            Console.WriteLine($"Average for 1: {total / m_products.Count}");
        }

        [TestMethod]
        public async Task Test10000GetUsingApiBatches()
        {
            await Initialize(10000);
            var sw = new Stopwatch();
            long total = 0;
            var batchSize = 1000;

            var tasks = new List<Task<IEnumerable<Product>>>();
            for (int i = 0; i < 10; i++)
            {
                var ids = m_products.Select(p => p.Id).Skip(i * batchSize).Take(batchSize);
                tasks.Add(GetProductBatch(ids));
            }
            sw.Start();
            var products = await Task.WhenAll(tasks);
            total = sw.ElapsedMilliseconds;
            Console.WriteLine($"Total time needed: {total}");
            Console.WriteLine($"Average for 1: {total / m_products.Count}");
        }

        private async Task<IEnumerable<Product>> GetProductBatch(IEnumerable<string> ids)
        {
            var response = await m_httpClient.PostAsync(
                $"{m_baseAddress}api/product/batch",
                new StringContent(JsonConvert.SerializeObject(ids), Encoding.UTF8, "application/json"));

            var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(await response.Content.ReadAsStringAsync());

            return products;
        }

        private async Task<Product> GetProduct(string id)
        {
            var response = await m_httpClient.GetAsync($"{m_baseAddress}api/product/{id}");
            if (HttpStatusCode.ServiceUnavailable == response.StatusCode)
            {
                while (response.StatusCode != HttpStatusCode.OK)
                    response = await m_httpClient.GetAsync($"{m_baseAddress}api/product/{id}");
            }
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Product prod = new Product();
            try
            {
                prod = JsonConvert.DeserializeObject<Product>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return prod;
        }
    }
}
