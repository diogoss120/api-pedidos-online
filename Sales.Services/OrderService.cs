using Sales.Domain;
using Sales.Persistence.Repositories.Entities;
using Sales.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.Services
{
    public class OrderService : IOrderService
    {
        private readonly UnitOfWork unitOfWork;

        public OrderService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Order> Add(Order order)
        {
            try
            {
                unitOfWork.OrderRepository.Add(order);

                if (await unitOfWork.CommitAsync())
                {
                    return await unitOfWork.OrderRepository.GetOrderByIdAsync(order.Id);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<bool> Delete(int orderId)
        {
            try
            {
                var order = unitOfWork.OrderRepository.GetOrderByIdAsync(orderId);
                if (order == null) throw new ArgumentException("Esse pedido não existe no banco de dados");

                unitOfWork.OrderRepository.Delete(order);
                return await unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task DeleteRange(int[] ordersId)
        {
            try
            {
                ICollection<Order> orders = new List<Order>();

                foreach (int id in ordersId)
                {
                    orders.Add(await GetOrderByIdAsync(id));
                }

                unitOfWork.ProductRepository.DeleteRange(orders.ToArray());
                await unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<Order> GetOrderByIdAsync(int id, bool includeProducts = false, bool includeCustomer = false)
        {
            try
            {
                var order = unitOfWork.OrderRepository.GetOrderByIdAsync(id, includeProducts, includeCustomer);
                if (order == null) throw new ArgumentException("Esse pedido não exite na base de dados");

                return order;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<IEnumerable<Order>> OrderListAsync(bool includeProducts = false, bool includeCustomer = false)
        {
            try
            {
                var orders = unitOfWork.OrderRepository.OrderListAsync(includeProducts, includeCustomer);
                if (orders == null) return null;

                return orders;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Order> Update(int orderId, Order model)
        {
            try
            {
                var order = await GetOrderByIdAsync(orderId);
                if (order == null) return null;

                unitOfWork.OrderRepository.Update(model);
                if (await unitOfWork.CommitAsync())
                {
                    return await GetOrderByIdAsync(model.Id);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
