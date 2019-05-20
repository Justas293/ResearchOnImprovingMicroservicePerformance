using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using ProductServiceAPI.DbContexts;
using ProductServiceAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProductServiceAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext m_dbContext;
        private readonly IDistributedCache m_distributedCache;

        public ProductRepository(ProductContext dbContext,
            IDistributedCache redisCache)
        {
            m_dbContext = dbContext;
            //_dbContext.Database.Migrate();

            m_distributedCache = redisCache;
        }

        public Product GetProductById(string productId)
        {
            var cachedProduct = m_distributedCache.GetString(productId);

            if( null != cachedProduct )
            {
                return JsonConvert.DeserializeObject<Product>(cachedProduct);
            }
            else
            {
                var product = m_dbContext.Products.Find(productId);
                m_distributedCache.SetString(product.Id, JsonConvert.SerializeObject(product));
                return product;
            }
        }

        public IEnumerable<Product> GetProducts()
        {
            return m_dbContext.Products.ToList();
        }
    }
}