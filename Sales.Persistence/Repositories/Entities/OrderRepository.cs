using Microsoft.EntityFrameworkCore;
using Sales.Domain;
using Sales.Persistence.Context;
using Sales.Persistence.Repositories.Contracts.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.Persistence.Repositories.Entities
{
    public class OrderRepository : Repository, IOrderRepository
    {
        private readonly SalesContext context;
        public OrderRepository(SalesContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<Order> GetOrderByIdAsync(int id, bool includeProducts = false, bool includeCustomer = false)
        {
            var order = context.Orders.Where(o => o.Id == id).AsNoTracking();

            if (includeProducts)
                order = order.Include(o => o.Products);

            if(includeCustomer)
                order = order.Include(o => o.Customer);

            return await order.SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> OrderListAsync(bool includeProducts = false, bool includeCustomer = false)
        {
            IQueryable<Order> orders = context.Orders.AsNoTracking();

            if (includeProducts)
                orders = orders.Include(o => o.Products);

            if (includeCustomer)
                orders = orders.Include(o => o.Customer);

            return await orders.ToListAsync();
        }
    }
}
