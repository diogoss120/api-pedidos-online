using Sales.Persistence.Context;
using Sales.Persistence.Repositories.Contracts;

namespace Sales.Persistence.Repositories.Entities
{
    public class Repository : IRepository
    {
        private readonly SalesContext context;
        public Repository(SalesContext context)
        {
            this.context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entities) where T : class
        {
            context.RemoveRange(entities);
        }

        public void Update<T>(T entity) where T : class
        {
            context.Update(entity);
        }
    }
}
