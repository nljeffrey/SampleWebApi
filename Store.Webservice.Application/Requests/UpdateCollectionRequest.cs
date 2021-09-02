using System.Security.Claims;
using MediatR;

namespace Store.Webservice.Application.Requests
{
    /// <summary>
    /// Generic request to update a collection.
    /// </summary>
    /// <typeparam name="TEntity">Update entity type.</typeparam>
    /// <typeparam name="TResponse">Response type.</typeparam>
    public class UpdateCollectionRequest<TEntity, TResponse> : IRequest<TResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCollectionRequest{TEntity, TResponse}"/> class.
        /// </summary>
        /// <param name="update">Object to update.</param>
        /// <param name="user">User who initiated the update.</param>
        public UpdateCollectionRequest(TEntity update, ClaimsPrincipal user = null)
        {
            Update = update;
            User = user;
        }

        /// <summary>
        /// Gets the object to update.
        /// </summary>
        public TEntity Update { get; }

        /// <summary>
        /// Gets the user who initiates the update.
        /// </summary>
        public ClaimsPrincipal User { get; }
    }
}