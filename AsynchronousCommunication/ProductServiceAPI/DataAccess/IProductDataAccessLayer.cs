using ProductServiceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductServiceAPI.DataAccess
{
    public interface IProductDataAccessLayer
    {
        IEnumerable<Product> GetProducts();
        Product GetProductById(string productId);
        void InsertProduct(Product product);
        void DeleteProduct(string productId);
        void UpdateProduct(Product product);
        Task<IEnumerable<Product>> GetProductsByIdsAsync(IEnumerable<string> ids);
        IEnumerable<Product> GetProductsByIds(IEnumerable<string> ids);
    }
}
