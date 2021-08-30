using Sales.Domain;
using Sales.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sales.Services.Contracts
{
    public interface IOrderService
    {
        Task<OrderDto[]> OrderListAsync(bool includeProducts = false, bool includeCustomer = false);
        Task<OrderDto> GetOrderByIdAsync(int id, bool includeProducts = false, bool includeCustomer = false);
        Task<OrderDto> Add(OrderDto order);
        Task<OrderDto> Update(int orderId, OrderDto order);
        Task<bool> Delete(int orderId);
        Task DeleteRange(int[] ordersId);
    }
}
