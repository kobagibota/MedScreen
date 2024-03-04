using System.Linq.Expressions;

namespace BaseLibrary.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
        
        T Get(Expression<Func<T, bool>> expression);
        Task<T> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
        
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        void Add(T entity);

        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        void AddRange(IEnumerable<T> entities);
        
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);

    }
}
