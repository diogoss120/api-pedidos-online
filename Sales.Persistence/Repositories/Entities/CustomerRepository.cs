using Microsoft.EntityFrameworkCore;
using Sales.Domain;
using Sales.Persistence.Context;
using Sales.Persistence.Repositories.Contracts.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.Persistence.Repositories.Entities
{
    public class CustomerRepository : Repository, ICustomerRepository
    {
        private readonly SalesContext context;
        public CustomerRepository(SalesContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Customer>> CustomerListAsync()
        {
            return await context.Customers.AsNoTracking().ToListAsync();
        }
        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await context.Customers.AsNoTracking()
                .Where(c => c.Id == id).FirstOrDefaultAsync();
        }
    }
}
