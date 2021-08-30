using Microsoft.EntityFrameworkCore;
using Sales.Domain;
using Sales.Persistence.Context;
using Sales.Persistence.Repositories.Contracts.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.Persistence.Repositories.Entities
{
    public class ProductRepository : Repository, IProductRepository
    {
        private readonly SalesContext context;
        public ProductRepository(SalesContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await context.Products.AsNoTracking()
                .Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> ProductListAsync()
        {
            return await context.Products.AsNoTracking().ToListAsync();
        }
    }
}
