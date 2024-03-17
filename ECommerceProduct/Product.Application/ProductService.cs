
using Product.Infrastructure.Interfaces;

namespace Product.Application
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Domain.Entities.Product>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products;
        }

        public async Task<Domain.Entities.Product> GetProduct(int Id)
        {
            var product = await _productRepository.GetByIdAsync(Id);
            return product;
        }

        public async Task<Domain.Entities.Product> CreateProduct(Domain.Entities.Product product)
        {
            await _productRepository.AddAsync(product);
            return product;
        }

        public async Task<Domain.Entities.Product> UpdateProduct(Domain.Entities.Product product)
        {
            await _productRepository.UpdateAsync(product);
            return product;
        }

        public async Task<int> DeleteProduct(int Id)
        {
            await _productRepository.DeleteAsync(Id);
            return Id;
        }
    }
}
