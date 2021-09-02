namespace Store.Webservice.Application.Interfaces.Repository
{
    /// <summary>
    /// Collection of methods to create a repository object.
    /// </summary>
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Gets a asynchronous repository with queries and commands.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <returns>The asynchronous repository object.</returns>
        IRepositoryAsync<T> GetRepositoryAsync<T>() where T : class;
    }
}