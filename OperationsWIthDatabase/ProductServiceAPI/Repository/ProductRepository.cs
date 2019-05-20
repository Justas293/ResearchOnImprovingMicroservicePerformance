using Microsoft.EntityFrameworkCore;
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
            //_dbContext.Database.Migrate();
        }
        public void DeleteProduct(string productId)
        {
            var product = m_dbContext.Products.Find(productId);
            m_dbContext.Products.Remove(product);
            Save();
        }

        public Product GetProductById(string productId)
        {
            return m_dbContext.Products.Find(productId);
        }

        public IEnumerable<Product> GetProducts()
        {
            return m_dbContext.Products.ToList();
        }

        public void InsertProduct(Product product)
        {
            m_dbContext.Add(product);
            Save();
        }

        public void Save()
        {
            m_dbContext.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            m_dbContext.Entry(product).State = EntityState.Modified;
            Save();
        }
    }
}