using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Store.Webservice.Application.Interfaces.Repository;

namespace Store.Webservice.Persistence.Repository
{
    /// <inheritdoc cref="IRepositoryAsync{T}" />
    public class RepositoryAsync<T> : BaseRepository<T>, IRepositoryAsync<T> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryAsync{T}"/> class.
        /// </summary>
        /// <param name="dbContext">Database context.</param>
        public RepositoryAsync(DbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            DbSet = DbContext.Set<T>();
        }

        /// <inheritdoc />
        public async Task<T> SingleAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            IEnumerable<Expression<Func<T, object>>> includes = null, bool disableTracking = true,
            CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = DbSet;
            query = QueryExtender(disableTracking, predicate, query, includes);

            if (orderBy != null)
            {
                return await orderBy(query).FirstOrDefaultAsync(cancellationToken);
            }

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<T>> AllAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            IEnumerable<Expression<Func<T, object>>> includes = null, bool disableTracking = true,
            CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = DbSet;
            query = QueryExtender(disableTracking, predicate, query, includes);

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync(cancellationToken);
            }

            return await query.ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            EntityEntry<T> entityEntry = await DbSet.AddAsync(entity, cancellationToken);
            return entityEntry.Entity;
        }

        /// <inheritdoc />
        public Task AddAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            return DbSet.AddRangeAsync(entities, cancellationToken);
        }

        /// <inheritdoc />
        public void UpdateAsync(T entity)
        {
            DbSet.Update(entity);
        }

        /// <inheritdoc />
        public void Delete(T entity)
        {
            T existing = DbSet.SingleOrDefault(x => x == entity);
            if (existing != null)
            {
                DbSet.Remove(existing);
            }
        }

        /// <inheritdoc />
        public void Delete(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }
    }
}