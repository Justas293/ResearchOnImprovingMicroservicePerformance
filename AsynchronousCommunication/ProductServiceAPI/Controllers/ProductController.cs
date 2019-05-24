using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductServiceAPI.DataAccess;
using ProductServiceAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger m_logger;
        private readonly IProductDataAccessLayer m_productDataAccessLayer;

        public ProductController(IProductDataAccessLayer productDataAccessLayer,
            ILogger<ProductController> logger)
        {
            m_productDataAccessLayer = productDataAccessLayer;
            m_logger = logger;
        }

        [HttpPost]
        [Route("prices")]
        public async Task<IActionResult> PostAsync(IEnumerable<string> productIds)
        {
            var products = await m_productDataAccessLayer.GetProductsByIdsAsync(productIds);

            var prices = products.Select(p => p.Price);

            return new OkObjectResult(prices);
        }
    }
}
