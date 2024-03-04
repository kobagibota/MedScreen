﻿using BaseLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using System.Linq.Expressions;

namespace ServerLibrary.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _dbContext;
        private readonly DbSet<T> _entitiySet;

        public GenericRepository(AppDbContext context)
        {
            _dbContext = context;
            _entitiySet = _dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            _dbContext.Add(entity);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(entity, cancellationToken);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbContext.AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddRangeAsync(entities, cancellationToken);
        }

        public T Get(Expression<Func<T, bool>> expression)
        {
            return _entitiySet.FirstOrDefault(expression);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _entitiySet.FirstOrDefaultAsync(expression, cancellationToken);
        }

        public IEnumerable<T> GetAll()
        {
            return _entitiySet.AsEnumerable();
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _entitiySet.ToListAsync(cancellationToken);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _entitiySet.Where(expression).AsEnumerable();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _entitiySet.Where(expression).ToListAsync(cancellationToken);
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
    }
}
