using Sales.Domain;
using Sales.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sales.Services.Contracts
{
    public interface ICustomerService
    {
        Task<CustomerDto> GetCustomerByIdAsync(int id);
        Task<IEnumerable<CustomerDto>> CustomerListAsync();
        Task<CustomerDto> Add(CustomerDto customer);
        Task<CustomerDto> Update(int customerId, CustomerDto customer);
        Task<bool> Delete(int customerId);
        Task DeleteRange(int[] customersId);
    }
}
