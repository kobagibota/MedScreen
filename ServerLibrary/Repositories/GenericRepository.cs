using BaseLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using System.Linq.Expressions;

namespace ServerLibrary.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region Private members

        protected readonly AppDbContext _dbContext;
        private readonly DbSet<T> _entitySet;

        #endregion

        #region Constructor

        public GenericRepository(AppDbContext context)
        {
            _dbContext = context;
            _entitySet = _dbContext.Set<T>();
        }

        #endregion

        #region Methods

        public async Task Add(T entity)
        {
            await _dbContext.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            await _dbContext.AddRangeAsync(entities);
        }

        public async Task<T?> GetBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> items = _entitySet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    items = items.Include(include);
                }
            }

            if (predicate != null)
            {
                return await items.FirstOrDefaultAsync(predicate);
            }
            else
            {
                return await items.FirstOrDefaultAsync();
            }
        }


        public async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> items = _entitySet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    items = items.Include(include);
                }
            }

            return await items.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetListBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> items = _entitySet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    items = items.Include(include);
                }
            }

            if (predicate != null)
            {
                items = items.Where(predicate);
            }

            return await items.ToListAsync();
        }


        public void Remove(T entity)
        {
            _dbContext.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbContext.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _dbContext.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _dbContext.UpdateRange(entities);
        }

        #endregion
    }
}
