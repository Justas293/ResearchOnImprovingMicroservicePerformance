using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        public ProductController(IProductRepository productRepository,
            ILogger<ProductController> logger)
        {
            m_productRepository = productRepository;
            m_logger = logger;
        }

        [HttpGet]
        [ResponseCache(Duration = 60)]
        public IActionResult Get()
        {
            var products = m_productRepository.GetProducts().ToList();
            return new OkObjectResult(products);
        }

        [HttpGet("{id}", Name = "Get")]
        [ResponseCache(Duration = 60)]
        public IActionResult Get(string id)
        {
            var product = m_productRepository.GetProductById(id);
            return new OkObjectResult(product);
        }
    }
}
