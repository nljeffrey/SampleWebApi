using System;

namespace Store.Webservice.Application.Interfaces.Repository
{
    /// <inheritdoc />
    /// <summary>
    /// Unit of work abstraction on top of repository.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets a asynchronous repository with queries and commands.
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <returns>The asynchronous repository object.</returns>
        IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class;

        /// <summary>
        /// Saves the changes in the database.
        /// </summary>
        /// <returns>Number of entities saved.</returns>
        int SaveChanges();
    }

    /// <inheritdoc />
    /// <typeparam name="TContext">Context type.</typeparam>
    public interface IUnitOfWork<out TContext> : IUnitOfWork
    {
        /// <summary>
        /// Gets the database context.
        /// </summary>
        TContext Context { get; }
    }
}