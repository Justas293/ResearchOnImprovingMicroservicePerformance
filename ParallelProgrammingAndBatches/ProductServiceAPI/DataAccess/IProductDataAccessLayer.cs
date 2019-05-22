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
        IEnumerable<Product> GetProductsByIds(IEnumerable<string> ids);
        void InsertProduct(Product product);
        void DeleteProduct(string productId);
        void UpdateProduct(Product product);
    }
}
