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
    public class ORMPerfTests
    {
        private readonly HttpClient m_httpClient;
        private readonly string m_baseAddress;

        public ORMPerfTests()
        {
            m_httpClient = new HttpClient();
            m_httpClient.DefaultRequestHeaders.Accept.Clear();
            m_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            m_baseAddress = "http://localhost:5001/";
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
        public async Task Test1InsertADO()
        {
            var sw = new Stopwatch();
            
            var product = new Product();
            product.Id = Guid.NewGuid().ToString();
            product.Name = "TestAdo";
            product.Description = "TestAdo";
            product.Price = 10;
            product.CategoryId = 1;

            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

            sw.Start();
            var response = await m_httpClient.PostAsync($"{m_baseAddress}api/product/ado", content);
            Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds}");
            sw.Stop();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Test1InsertEF()
        {
            var sw = new Stopwatch();

            var product = new Product();
            product.Id = Guid.NewGuid().ToString();
            product.Name = "TestEF";
            product.Description = "TestEF";
            product.Price = 10;
            product.CategoryId = 1;

            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

            sw.Start();
            var response = await m_httpClient.PostAsync($"{m_baseAddress}api/product/ef", content);
            Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds}");
            sw.Stop();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Test1UpdateADO()
        {
            var sw = new Stopwatch();

            var product = new Product();
            product.Id = Guid.NewGuid().ToString();
            product.Name = "TestAdo";
            product.Description = "TestAdo";
            product.Price = 10;
            product.CategoryId = 1;

            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

            var response = await m_httpClient.PostAsync($"{m_baseAddress}api/product/ado", content);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            product.Name = "TestAdoUpdated";
            product.Description = "TestAdoUpdated";
            product.Price = 20;
            product.CategoryId = 2;

            content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

            response = await m_httpClient.PutAsync($"{m_baseAddress}api/product/ado", content);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Test1UpdateEF()
        {
            var sw = new Stopwatch();

            var product = new Product();
            product.Id = Guid.NewGuid().ToString();
            product.Name = "TestEF";
            product.Description = "TestEF";
            product.Price = 10;
            product.CategoryId = 1;

            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

            var response = await m_httpClient.PostAsync($"{m_baseAddress}api/product/ef", content);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            product.Name = "TestEFUpdated";
            product.Description = "TestEFUpdated";
            product.Price = 20;
            product.CategoryId = 2;

            content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

            response = await m_httpClient.PutAsync($"{m_baseAddress}api/product/ef", content);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Test1DeleteEF()
        {
            var sw = new Stopwatch();

            var product = new Product();
            product.Id = Guid.NewGuid().ToString();
            product.Name = "TestEF";
            product.Description = "TestEF";
            product.Price = 10;
            product.CategoryId = 1;

            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

            var response = await m_httpClient.PostAsync($"{m_baseAddress}api/product/ef", content);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            response = await m_httpClient.DeleteAsync($"{m_baseAddress}api/product/ef/{product.Id}");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Test1DeleteADO()
        {
            var sw = new Stopwatch();

            var product = new Product();
            product.Id = Guid.NewGuid().ToString();
            product.Name = "TestADO";
            product.Description = "TestADO";
            product.Price = 10;
            product.CategoryId = 1;

            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

            var response = await m_httpClient.PostAsync($"{m_baseAddress}api/product/ado", content);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            response = await m_httpClient.DeleteAsync($"{m_baseAddress}api/product/ado/{product.Id}");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Test1000InsertAdo()
        {
            var sw = new Stopwatch();
            long totalTimeNeeded = 0;

            var contents = new List<StringContent>();
            for (int i = 0; i < 1000; i++)
            {
                var product = new Product();
                product.Id = Guid.NewGuid().ToString();
                product.Name = "TestAdo";
                product.Description = "TestAdo";
                product.Price = 10;
                product.CategoryId = 1;

                var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                contents.Add(content);
            }

            sw.Start();
            foreach (var content in contents)
            {
                sw.Restart();
                var response = await m_httpClient.PostAsync($"{m_baseAddress}api/product/ado", content);
                totalTimeNeeded += sw.ElapsedMilliseconds;
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            }
            Console.WriteLine($"Total time needed: {totalTimeNeeded}");
            Console.WriteLine($"Average time needed for 1 insert: {totalTimeNeeded / 1000}");
        }

        [TestMethod]
        public async Task Test1000InsertEF()
        {
            var sw = new Stopwatch();
            long totalTimeNeeded = 0;

            var contents = new List<StringContent>();
            for (int i = 0; i < 1000; i++)
            {
                var product = new Product();
                product.Id = Guid.NewGuid().ToString();
                product.Name = "TestEF";
                product.Description = "TestEF";
                product.Price = 10;
                product.CategoryId = 1;

                var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                contents.Add(content);
            }

            sw.Start();
            foreach (var content in contents)
            {
                sw.Restart();
                var response = await m_httpClient.PostAsync($"{m_baseAddress}api/product/ef", content);
                totalTimeNeeded += sw.ElapsedMilliseconds;
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            }
            Console.WriteLine($"Total time needed: {totalTimeNeeded}");
            Console.WriteLine($"Average time needed for 1 insert: {totalTimeNeeded / 1000}");
        }

        [TestMethod]
        public async Task Test1000UpdateAdo()
        {
            var sw = new Stopwatch();
            long totalTimeNeeded = 0;

            var contents = new List<StringContent>();
            var products = new List<Product>();
            for (int i = 0; i < 1000; i++)
            {
                var product = new Product();
                product.Id = Guid.NewGuid().ToString();
                product.Name = "TestAdo";
                product.Description = "TestAdo";
                product.Price = 10;
                product.CategoryId = 1;
                products.Add(product);

                var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                contents.Add(content);
            }

            foreach (var content in contents)
            {
                var response = await m_httpClient.PostAsync($"{m_baseAddress}api/product/ado", content);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            contents.Clear();
            foreach(var p in products)
            {
                p.Name = "Updated";
                p.Description = "Updated";
                p.Price = 20;
                p.CategoryId = 2;

                var content = new StringContent(JsonConvert.SerializeObject(p), Encoding.UTF8, "application/json");
                contents.Add(content);
            }

            sw.Start();
            foreach (var content in contents)
            {
                sw.Restart();
                var response = await m_httpClient.PutAsync($"{m_baseAddress}api/product/ado", content);
                totalTimeNeeded += sw.ElapsedMilliseconds;
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
            Console.WriteLine($"Total time needed: {totalTimeNeeded}");
            Console.WriteLine($"Average time needed for 1 update: {totalTimeNeeded / 1000}");
        }

        [TestMethod]
        public async Task Test1000UpdateEF()
        {
            var sw = new Stopwatch();
            long totalTimeNeeded = 0;

            var contents = new List<StringContent>();
            var products = new List<Product>();
            for (int i = 0; i < 1000; i++)
            {
                var product = new Product();
                product.Id = Guid.NewGuid().ToString();
                product.Name = "TestEF";
                product.Description = "TestEF";
                product.Price = 10;
                product.CategoryId = 1;
                products.Add(product);

                var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                contents.Add(content);
            }

            foreach (var content in contents)
            {
                var response = await m_httpClient.PostAsync($"{m_baseAddress}api/product/ado", content);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            contents.Clear();
            foreach (var p in products)
            {
                p.Name = "Updated";
                p.Description = "Updated";
                p.Price = 20;
                p.CategoryId = 2;

                var content = new StringContent(JsonConvert.SerializeObject(p), Encoding.UTF8, "application/json");
                contents.Add(content);
            }

            sw.Start();
            foreach (var content in contents)
            {
                sw.Restart();
                var response = await m_httpClient.PutAsync($"{m_baseAddress}api/product/ef", content);
                totalTimeNeeded += sw.ElapsedMilliseconds;
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
            Console.WriteLine($"Total time needed: {totalTimeNeeded}");
            Console.WriteLine($"Average time needed for 1 update: {totalTimeNeeded / 1000}");
        }

        [TestMethod]
        public async Task Test1000DeleteADO()
        {
            var sw = new Stopwatch();
            long totalTimeNeeded = 0;

            var contents = new List<StringContent>();
            var products = new List<Product>();
            for (int i = 0; i < 1000; i++)
            {
                var product = new Product();
                product.Id = Guid.NewGuid().ToString();
                product.Name = "TestADO";
                product.Description = "TestADO";
                product.Price = 10;
                product.CategoryId = 1;
                products.Add(product);

                var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                contents.Add(content);
            }

            foreach (var content in contents)
            {
                var response = await m_httpClient.PostAsync($"{m_baseAddress}api/product/ado", content);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            sw.Start();
            foreach (var p in products)
            {
                sw.Restart();
                var response = await m_httpClient.DeleteAsync($"{m_baseAddress}api/product/ado/{p.Id}");
                totalTimeNeeded += sw.ElapsedMilliseconds;
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
            Console.WriteLine($"Total time needed: {totalTimeNeeded}");
            Console.WriteLine($"Average time needed for 1 delete: {totalTimeNeeded / 1000}");
        }

        [TestMethod]
        public async Task Test1000DeleteEF()
        {
            var sw = new Stopwatch();
            long totalTimeNeeded = 0;

            var contents = new List<StringContent>();
            var products = new List<Product>();
            for (int i = 0; i < 1000; i++)
            {
                var product = new Product();
                product.Id = Guid.NewGuid().ToString();
                product.Name = "TestEF";
                product.Description = "TestEF";
                product.Price = 10;
                product.CategoryId = 1;
                products.Add(product);

                var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                contents.Add(content);
            }

            foreach (var content in contents)
            {
                var response = await m_httpClient.PostAsync($"{m_baseAddress}api/product/ado", content);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            sw.Start();
            foreach (var p in products)
            {
                sw.Restart();
                var response = await m_httpClient.DeleteAsync($"{m_baseAddress}api/product/ef/{p.Id}");
                totalTimeNeeded += sw.ElapsedMilliseconds;
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            Console.WriteLine($"Total time needed: {totalTimeNeeded}");
            Console.WriteLine($"Average time needed for 1 delete: {totalTimeNeeded / 1000}");
        }

        [TestMethod]
        public async Task Test1000GetEF()
        {
            var sw = new Stopwatch();
            long totalTimeNeeded = 0;

            var contents = new List<StringContent>();
            var products = new List<Product>();
            for (int i = 0; i < 1000; i++)
            {
                var product = new Product();
                product.Id = Guid.NewGuid().ToString();
                product.Name = "TestEF";
                product.Description = "TestEF";
                product.Price = 10;
                product.CategoryId = 1;
                products.Add(product);

                var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                contents.Add(content);
            }

            foreach (var content in contents)
            {
                var response = await m_httpClient.PostAsync($"{m_baseAddress}api/product/ado", content);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            sw.Start();
            foreach (var p in products)
            {
                sw.Restart();
                var response = await m_httpClient.GetAsync($"{m_baseAddress}api/product/efget/{p.Id}");
                totalTimeNeeded += sw.ElapsedMilliseconds;
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            Console.WriteLine($"Total time needed: {totalTimeNeeded}");
            Console.WriteLine($"Average time needed for 1 get: {totalTimeNeeded / 1000}");
        }

        [TestMethod]
        public async Task Test1000GetADO()
        {
            var sw = new Stopwatch();
            long totalTimeNeeded = 0;

            var contents = new List<StringContent>();
            var products = new List<Product>();
            for (int i = 0; i < 1000; i++)
            {
                var product = new Product();
                product.Id = Guid.NewGuid().ToString();
                product.Name = "TestADO";
                product.Description = "TestADO";
                product.Price = 10;
                product.CategoryId = 1;
                products.Add(product);

                var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                contents.Add(content);
            }

            foreach (var content in contents)
            {
                var response = await m_httpClient.PostAsync($"{m_baseAddress}api/product/ado", content);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            sw.Start();
            foreach (var p in products)
            {
                sw.Restart();
                var response = await m_httpClient.GetAsync($"{m_baseAddress}api/product/adoget/{p.Id}");
                totalTimeNeeded += sw.ElapsedMilliseconds;
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            Console.WriteLine($"Total time needed: {totalTimeNeeded}");
            Console.WriteLine($"Average time needed for 1 get: {totalTimeNeeded / 1000}");
        }
    }
}
