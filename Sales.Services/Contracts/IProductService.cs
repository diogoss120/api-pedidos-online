using Sales.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sales.Services.Contracts
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> ProductListAsync();
        Task<Product> Add(Product product);
        Task<Product> Update(int productId, Product product);
        Task<bool> Delete(int productId);
        Task DeleteRange(int[] productsId);
    }
}
