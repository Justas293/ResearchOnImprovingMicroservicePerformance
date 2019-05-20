using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductServiceAPI.Models;
using ProductServiceAPI.Repository;
using System.Diagnostics;
using System.Linq;
using System.Transactions;

namespace ProductServiceAPI.Controllers
{
    [Route("")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger m_logger; 
        private readonly IProductRepository m_productRepository;

        public ProductController(IProductRepository productRepository,
            ILogger<ProductController> logger)
        {
            m_productRepository = productRepository;
            m_logger = logger;
        }

        [Route("websurge-allow.txt")]
        public IActionResult AllowWebSurge()
        {
            return new OkObjectResult("");
        }

        [Route("api/[controller]")]
        [HttpGet]
        public IActionResult Get()
        {
            var products = m_productRepository.GetProducts().ToList();
            return new OkObjectResult(products);
        }

        [Route("api/[controller]/{id}")]
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(string id)
        {
            var product = m_productRepository.GetProductById(id);
            return new OkObjectResult(product);
        }
    }
}
