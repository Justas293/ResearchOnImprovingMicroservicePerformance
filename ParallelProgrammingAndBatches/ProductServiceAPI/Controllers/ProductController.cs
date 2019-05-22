using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductServiceAPI.DataAccess;
using ProductServiceAPI.Models;
using System.Collections.Generic;
using System.Linq;

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

        [HttpGet]
        public IActionResult Get()
        {
            var products = m_productDataAccessLayer.GetProducts().ToList();
            return new OkObjectResult(products);
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(string id)
        {
            var product = m_productDataAccessLayer.GetProductById(id);
            return new OkObjectResult(product);
        }

        [HttpPost]
        [Route("batch")]
        public IActionResult GetBatch(IEnumerable<string> ids)
        {
            var prods = m_productDataAccessLayer.GetProductsByIds(ids);
            return new OkObjectResult(prods);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            m_productDataAccessLayer.InsertProduct(product);
            return Ok();
        }

        [HttpPut]
        public IActionResult Put([FromBody] Product product)
        {
            if (product != null)
            {
                m_productDataAccessLayer.UpdateProduct(product);
                return Ok();
            }
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            m_productDataAccessLayer.DeleteProduct(id);
            return new OkResult();
        }
    }
}
