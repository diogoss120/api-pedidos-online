using Sales.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sales.Services.Contracts
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> OrderListAsync(bool includeProducts = false, bool includeCustomer = false);
        Task<Order> GetOrderByIdAsync(int id, bool includeProducts = false, bool includeCustomer = false);
        Task<Order> Add(Order order);
        Task<Order> Update(int orderId, Order order);
        Task<bool> Delete(int orderId);
        Task DeleteRange(int[] ordersId);
    }
}
