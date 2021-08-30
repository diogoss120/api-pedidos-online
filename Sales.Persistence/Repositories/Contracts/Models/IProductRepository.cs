using Sales.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sales.Persistence.Repositories.Contracts.Models
{
    public interface IProductRepository : IRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> ProductListAsync();
    }
}
