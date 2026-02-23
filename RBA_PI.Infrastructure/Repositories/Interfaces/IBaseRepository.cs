using System.Linq.Expressions;


namespace RBA_PI.Infrastructure.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<int> UpdateAsync(Expression<Func<T, bool>> predicate, IDictionary<string, object?> fields);
    }
}
