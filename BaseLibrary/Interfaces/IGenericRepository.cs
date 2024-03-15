using System.Linq.Expressions;

namespace BaseLibrary.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] include);

        Task<IEnumerable<T>> GetListBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] include);

        Task<T?> GetBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] include);

        Task Add(T entity);

        Task AddRange(IEnumerable<T> entities);
        
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);

    }
}
