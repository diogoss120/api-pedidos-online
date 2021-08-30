using Sales.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sales.Persistence.Repositories.Contracts.Models
{
    public interface IOrderRepository : IRepository
    {
        Task<Order> GetOrderByIdAsync(int id, bool includeProducts = false, bool includeCustomer = false);
        Task<IEnumerable<Order>> OrderListAsync(bool includeProducts = false, bool includeCustomer = false);
    }
}
