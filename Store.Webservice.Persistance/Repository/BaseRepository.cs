using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Store.Webservice.Persistence.Repository
{
    /// <summary>
    /// Base repository to implement shared query extender.
    /// </summary>
    /// <typeparam name="T">Entity type.</typeparam>
    public class BaseRepository<T> where T : class
    {
        /// <summary>
        /// Database context.
        /// </summary>
        internal DbContext DbContext;

        /// <summary>
        /// Database set.
        /// </summary>
        internal DbSet<T> DbSet;

        /// <summary>
        /// Extends the given query with eager loading options.
        /// </summary>
        /// <param name="disableTracking">Checks whether tracking is disabled.</param>
        /// <param name="predicate">Expression with the predicate to filter the query with.</param>
        /// <param name="query">Given query.</param>
        /// <param name="includes">List of include expressions.</param>
        /// <returns>Query with the added filters and options.</returns>
        internal static IQueryable<T> QueryExtender(bool disableTracking, Expression<Func<T, bool>> predicate,
            IQueryable<T> query, IEnumerable<Expression<Func<T, object>>> includes = null)
        {
            if (disableTracking)
            {
                query = query.AsTracking();
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query;
        }

        /// <summary>
        /// Extends the given query with eager loading options.
        /// </summary>
        /// <param name="disableTracking">Checks whether tracking is disabled.</param>
        /// <param name="predicate">Expression with the predicate to filter the query with.</param>
        /// <param name="query">Given query.</param>
        /// <param name="includes">List of include expressions.</param>
        /// <typeparam name="TInclude">Include type.</typeparam>
        /// <typeparam name="TThenInclude">Then include type.</typeparam>
        /// <returns>Query with the added filters and options.</returns>
        internal static IQueryable<T> QueryExtender<TInclude, TThenInclude>(bool disableTracking,
            Expression<Func<T, bool>> predicate, IQueryable<T> query,
            IEnumerable<(Expression<Func<T, TInclude>> include, Expression<Func<TInclude, TThenInclude>>
                thenInclude)> includes = null)
        {
            if (disableTracking)
            {
                query = query.AsTracking();
            }

            if (includes != null)
            {
                query = includes.Aggregate(query,
                    (current, includePair) =>
                        current.Include(includePair.include).ThenInclude(includePair.thenInclude));
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query;
        }

        /// <summary>
        /// Extends the given query with eager loading options.
        /// </summary>
        /// <param name="disableTracking">Checks whether tracking is disabled.</param>
        /// <param name="predicate">Expression with the predicate to filter the query with.</param>
        /// <param name="query">Given query.</param>
        /// <param name="includes">List of include expressions.</param>
        /// <typeparam name="TInclude">Include type (collection).</typeparam>
        /// <typeparam name="TThenInclude">Then include type.</typeparam>
        /// <returns>Query with the added filters and options.</returns>
        internal static IQueryable<T> QueryExtender<TInclude, TThenInclude>(bool disableTracking,
            Expression<Func<T, bool>> predicate, IQueryable<T> query,
            IEnumerable<(Expression<Func<T, ICollection<TInclude>>> include, Expression<Func<TInclude, TThenInclude>>
                thenInclude)> includes = null)
        {
            if (disableTracking)
            {
                query = query.AsTracking();
            }

            if (includes != null)
            {
                query = includes.Aggregate(query,
                    (current, includePair) =>
                        current.Include(includePair.include).ThenInclude(includePair.thenInclude));
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query;
        }
    }
}