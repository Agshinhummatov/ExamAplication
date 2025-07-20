using Application.Abstractions.Repositories;
using Domain.Base;
using Microsoft.EntityFrameworkCore.Storage;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ExamDbContext _context;
        private readonly Dictionary<Type, object> _repositories = new();
        private IDbContextTransaction? _transaction;

        public UnitOfWork(ExamDbContext context)
        {
            _context = context;
        }

        public IReadRepository<T> GetReadRepository<T>() where T : BaseEntity
        {
            if (_repositories.TryGetValue(typeof(T), out var repository) && repository is IReadRepository<T> readRepo)
                return readRepo;

            var newRepo = new EfReadRepository<T>(_context);
            _repositories[typeof(T)] = newRepo;
            return newRepo;
        }

        public IWriteRepository<T> GetWriteRepository<T>() where T : BaseEntity
        {
            if (_repositories.TryGetValue(typeof(T), out var repository) && repository is IWriteRepository<T> writeRepo)
                return writeRepo;

            var newRepo = new EfWriteRepository<T>(_context);
            _repositories[typeof(T)] = newRepo;
            return newRepo;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_transaction == null)
            {
                _transaction = await _context.Database.BeginTransactionAsync();
            }

            return _transaction;
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction == null)
                throw new InvalidOperationException("No active transaction found.");

            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction == null)
                throw new InvalidOperationException("No active transaction found.");

            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }

}