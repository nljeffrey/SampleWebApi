using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Store.Webservice.Application.Interfaces.Repository;

namespace Store.Webservice.Persistence.Repository
{
    /// <inheritdoc cref="IUnitOfWork{TContext}" />
    public class UnitOfWork<TContext> : IRepositoryFactory, IUnitOfWork<TContext>
        where TContext : DbContext, IDisposable
    {
        private Dictionary<string, object> _repositories;

        private bool _disposed;

        /// <summary>
        /// Gets the database context.
        /// </summary>
        public TContext Context { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork{TContext}"/> class.
        /// </summary>
        /// <param name="context">Database context.</param>
        public UnitOfWork(TContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc cref="IUnitOfWork{TContext}" />
        public IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class
        {
            _repositories ??= new Dictionary<string, object>();

            Type type = typeof(TEntity);
            string key = $"async_{type}";
            if (!_repositories.ContainsKey(key))
            {
                _repositories[key] = new RepositoryAsync<TEntity>(Context);
            }

            return (IRepositoryAsync<TEntity>)_repositories[key];
        }

        /// <inheritdoc />
        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the db context object.
        /// </summary>
        /// <param name="disposing">Checks whether the db context must be disposed or not.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                Context?.Dispose();
            }

            _disposed = true;
        }
    }
}