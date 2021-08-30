using System;
using System.Threading.Tasks;

namespace Sales.Persistence.Repositories.Contracts
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
    }
}
