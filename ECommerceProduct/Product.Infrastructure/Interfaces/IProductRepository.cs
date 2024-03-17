using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Product.Infrastructure.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Domain.Entities.Product>> GetAllAsync();
        Task<Domain.Entities.Product> GetByIdAsync(int id);
        Task AddAsync(Domain.Entities.Product product);
        Task UpdateAsync(Domain.Entities.Product product);
        Task DeleteAsync(int id);
    }
}
