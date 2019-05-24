using ProductServiceAPI.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductServiceAPI.Services
{
    public class ProductService
    {
        private readonly IProductDataAccessLayer m_productDataAccessLayer;

        public ProductService(IProductDataAccessLayer productDataAccessLayer)
        {
            m_productDataAccessLayer = productDataAccessLayer;
        }

        public IEnumerable<decimal> GetProductPrices(IEnumerable<string> productIds)
        {
            var products = m_productDataAccessLayer.GetProductsByIds(productIds);

            var prices = products.Select(p => p.Price);

            return prices;
        }
    }
}
