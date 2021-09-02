using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Webservice.Application.Interfaces.Repository
{
    /// <summary>
    /// Collection of repository asynchronous methods.
    /// </summary>
    /// <typeparam name="T">Entity type.</typeparam>
    public interface IRepositoryAsync<T> where T : class
    {
        /// <summary>
        /// Gets a single entity type object.
        /// </summary>
        /// <param name="predicate">Predicate with the linq expression.</param>
        /// <param name="orderBy">Order by result function.</param>
        /// <param name="includes">List of include expressions.</param>
        /// <param name="disableTracking">Checks whether tracking is disabled or not.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task with the entity type object.</returns>
        Task<T> SingleAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            IEnumerable<Expression<Func<T, object>>> includes = null, bool disableTracking = true,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all the entity type objects.
        /// </summary>
        /// <param name="predicate">Predicate with the linq expression.</param>
        /// <param name="orderBy">Order by result function.</param>
        /// <param name="includes">List of include expressions.</param>
        /// <param name="disableTracking">Checks whether tracking is disabled or not.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task with the collection of the entity type objects.</returns>
        Task<IEnumerable<T>> AllAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            IEnumerable<Expression<Func<T, object>>> includes = null, bool disableTracking = true,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a new entity type object to the database.
        /// </summary>
        /// <param name="entity">Entity type object to add.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task with added entity object.</returns>
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a collection of entity type objects.
        /// </summary>
        /// <param name="entities">Collections of objects to add.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task.</returns>
        Task AddAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the given entity type object.
        /// </summary>
        /// <param name="entity">The entity type object to update.</param>
        void UpdateAsync(T entity);

        /// <summary>
        /// Deletes the given entity type object.
        /// </summary>
        /// <param name="entity">The entity type object to delete.</param>
        void Delete(T entity);

        /// <summary>
        /// Deletes a collection of entity type objects.
        /// </summary>
        /// <param name="entities">Collections of objects to delete.</param>
        void Delete(IEnumerable<T> entities);
    }
}