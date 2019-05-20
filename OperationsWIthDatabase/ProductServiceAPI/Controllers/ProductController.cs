using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductServiceAPI.DataAccess;
using ProductServiceAPI.Models;
using ProductServiceAPI.Repository;
using System.Linq;

namespace ProductServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger m_logger;
        private readonly IProductRepository m_productRepository;
        private readonly IProductDataAccessLayer m_productDataAccessLayer;

        public ProductController(IProductRepository productRepository,
            IProductDataAccessLayer productDataAccessLayer,
            ILogger<ProductController> logger)
        {
            m_productRepository = productRepository;
            m_productDataAccessLayer = productDataAccessLayer;
            m_logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var products = m_productRepository.GetProducts().ToList();
            return new OkObjectResult(products);
        }

        [HttpGet("{id}", Name = "Get")]
        [Route("adoget/{id}")]
        public IActionResult GetAdo(string id)
        {
            var product = m_productDataAccessLayer.GetProductById(id);
            return new OkObjectResult(product);
        }

        [HttpGet("{id}", Name = "Get")]
        [Route("efget/{id}")]
        public IActionResult GetEF(string id)
        {
            var product = m_productRepository.GetProductById(id);
            return new OkObjectResult(product);
        }

        [HttpPost]
        [Route("ado")]
        public IActionResult PostADO([FromBody] Product product)
        {
            m_productDataAccessLayer.InsertProduct(product);
            return Ok();
        }

        [HttpPost]
        [Route("ef")]
        public IActionResult PostEF([FromBody] Product product)
        {
            m_productRepository.InsertProduct(product);
            return Ok();
        }

        [HttpPut]
        [Route("ado")]
        public IActionResult PutAdo([FromBody] Product product)
        {
            if (product != null)
            {
                m_productDataAccessLayer.UpdateProduct(product);
                return Ok();
            }
            return new NoContentResult();
        }

        [HttpPut]
        [Route("ef")]
        public IActionResult PutEf([FromBody] Product product)
        {
            if (product != null)
            {
                m_productRepository.UpdateProduct(product);
                return Ok();
            }
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        [Route("ado/{id}")]
        public IActionResult DeleteAdo(string id)
        {
            m_productDataAccessLayer.DeleteProduct(id);
            return new OkResult();
        }

        [HttpDelete("{id}")]
        [Route("ef/{id}")]
        public IActionResult DeleteEf(string id)
        {
            m_productRepository.DeleteProduct(id);
            return new OkResult();
        }
    }
}
