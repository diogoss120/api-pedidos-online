using Sales.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sales.Services.Contracts
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<IEnumerable<Customer>> CustomerListAsync();
        Task<Customer> Add(Customer customer);
        Task<Customer> Update(int customerId, Customer customer);
        Task<bool> Delete(int customerId);
        Task DeleteRange(int[] customersId);
    }
}
