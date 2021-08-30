using Sales.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sales.Persistence.Repositories.Contracts.Models
{
    public interface ICustomerRepository : IRepository
    {
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<IEnumerable<Customer>> CustomerListAsync();
    }
}
