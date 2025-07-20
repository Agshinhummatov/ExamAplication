using Domain.Base;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Abstractions.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IReadRepository<T> GetReadRepository<T>() where T : BaseEntity;
        IWriteRepository<T> GetWriteRepository<T>() where T : BaseEntity;

        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

        Task<int> SaveChangesAsync();
    }
}