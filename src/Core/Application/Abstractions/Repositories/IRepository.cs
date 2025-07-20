using Domain.Base;
using System.Linq.Expressions;

namespace Application.Abstractions.Repositories
{
    public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T> where T : BaseEntity
    {

    }
}