using ProductServiceAPI.DbContexts;
using ProductServiceAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProductServiceAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext m_dbContext;

        public ProductRepository(ProductContext dbContext)
        {
            m_dbContext = dbContext;
            //m_dbContext.Database.Migrate();
        }

        public Product GetProductById(string productId)
        {
            return m_dbContext.Products.Find(productId);
        }

        public IEnumerable<Product> GetProducts()
        {
            return m_dbContext.Products.ToList();
        }
    }
}