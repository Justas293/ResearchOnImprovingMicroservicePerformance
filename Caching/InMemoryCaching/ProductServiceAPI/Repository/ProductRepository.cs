using Microsoft.Extensions.Caching.Memory;
using ProductServiceAPI.DbContexts;
using ProductServiceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductServiceAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext m_dbContext;
        private readonly IMemoryCache m_cache;

        public ProductRepository(ProductContext dbContext,
            IMemoryCache cache)
        {
            m_dbContext = dbContext;
            //m_dbContext.Database.Migrate();
            m_cache = cache;
        }

        public Product GetProductById(string productId)
        {
            var cachedProduct = m_cache.Get<Product>(productId);
            if (null != cachedProduct)
            {
                return cachedProduct;
            }
            else
            {
                var product = m_dbContext.Products.Find(productId);

                var entryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30));

                m_cache.Set(product.Id, product, entryOptions);

                return product;
            }
        }

        public IEnumerable<Product> GetProducts()
        {
            return m_dbContext.Products.ToList();
        }
    }
}