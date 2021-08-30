using AutoMapper;
using Sales.Domain;
using Sales.Persistence.Repositories.Entities;
using Sales.Services.Contracts;
using Sales.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.Services
{
    public class OrderService : IOrderService
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public OrderService(UnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<OrderDto> Add(OrderDto orderDto)
        {
            try
            {
                var model = mapper.Map<Order>(orderDto);
                unitOfWork.OrderRepository.Add(model);

                if (await unitOfWork.CommitAsync())
                {
                    var response = await unitOfWork.OrderRepository.GetOrderByIdAsync(model.Id);
                    return mapper.Map<OrderDto>(response);
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
                var order = await unitOfWork.OrderRepository.GetOrderByIdAsync(orderId);
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
                    orders.Add(await unitOfWork.OrderRepository.GetOrderByIdAsync(id));
                }

                unitOfWork.ProductRepository.DeleteRange(orders.ToArray());
                await unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id, bool includeProducts = false, bool includeCustomer = false)
        {
            try
            {
                var order = await unitOfWork.OrderRepository.GetOrderByIdAsync(id, includeProducts, includeCustomer);
                if (order == null) return null;

                return mapper.Map<OrderDto>(order);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<OrderDto[]> OrderListAsync(bool includeProducts = false, bool includeCustomer = false)
        {
            try
            {
                var orders = await unitOfWork.OrderRepository.OrderListAsync(includeProducts, includeCustomer);
                if (orders == null) return null;

                return mapper.Map<OrderDto[]>(orders);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<OrderDto> Update(int orderId, OrderDto orderDto)
        {
            try
            {
                var order = await unitOfWork.OrderRepository.GetOrderByIdAsync(orderId);
                if (order == null) return null;

                var model = mapper.Map<Order>(orderDto);
                unitOfWork.OrderRepository.Update(model);
                if (await unitOfWork.CommitAsync())
                {
                    var response = await unitOfWork.OrderRepository.GetOrderByIdAsync(model.Id);
                    return mapper.Map<OrderDto>(response);
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
