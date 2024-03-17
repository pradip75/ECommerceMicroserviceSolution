using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application
{
    public interface IProductService
    {
        Task<IEnumerable<Domain.Entities.Product>> GetAllProductsAsync();
        Task<Domain.Entities.Product> GetProduct(int Id);
        Task<Domain.Entities.Product> CreateProduct(Domain.Entities.Product product);
        Task<Domain.Entities.Product> UpdateProduct(Domain.Entities.Product product);
        Task<int> DeleteProduct(int Id);
    }
}
