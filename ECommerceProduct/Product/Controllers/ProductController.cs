using Microsoft.AspNetCore.Mvc;
using Product.Infrastructure.Interfaces;

namespace ECommerceProduct.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("getallproducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("getproduct/{Id:int}")]
        public async Task<IActionResult> GetProduct(int Id)
        {
            var product = await _productRepository.GetByIdAsync(Id);
            return Ok(product);
        }

        [HttpPost("createproduct")]
        public async Task<IActionResult> CreateProduct([FromBody] Product.Domain.Entities.Product product)
        {
            await _productRepository.AddAsync(product);
            return Ok(product);
        }

        [HttpPut("updateproduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product.Domain.Entities.Product product)
        {
            await _productRepository.UpdateAsync(product);
            return Ok(product);
        }

        [HttpDelete("deleteproduct{Id:int}")]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            await _productRepository.DeleteAsync(Id);
            return Ok(Id);
        }
    }
}

