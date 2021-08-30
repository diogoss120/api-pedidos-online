using Sales.Persistence.Context;
using Sales.Persistence.Repositories.Contracts;
using Sales.Persistence.Repositories.Contracts.Models;
using System.Threading.Tasks;

namespace Sales.Persistence.Repositories.Entities
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SalesContext context;
        public readonly ICustomerRepository CustomerRepository;
        public readonly IOrderRepository OrderRepository;
        public readonly IProductRepository ProductRepository;
        public UnitOfWork(SalesContext context, ICustomerRepository customerRepository,
                        IOrderRepository orderRepository, IProductRepository productRepository)
        {
            this.context = context;
            this.CustomerRepository = customerRepository;
            this.OrderRepository = orderRepository;
            this.ProductRepository = productRepository;
        }
        public async Task<bool> CommitAsync()
        {
            return (await context.SaveChangesAsync()) > 0;
        }
    }
}
