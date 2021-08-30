using Sales.Domain;
using Sales.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sales.Services.Contracts
{
    public interface IProductService
    {
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductDto>> ProductListAsync();
        Task<ProductDto> Add(ProductDto product);
        Task<ProductDto> Update(int productId, ProductDto product);
        Task<bool> Delete(int productId);
        Task DeleteRange(int[] productsId);
    }
}
