using System.Security.Claims;
using MediatR;

namespace Store.Webservice.Application.Requests
{
    /// <summary>
    /// Generic request to delete an existing object.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    public class DeleteRequest<TEntity> : IRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRequest{TEntity}"/> class.
        /// </summary>
        /// <param name="delete">Object to delete.</param>
        /// <param name="user">Claims principal user.</param>
        public DeleteRequest(TEntity delete, ClaimsPrincipal user = null)
        {
            Delete = delete;
            User = user;
        }

        /// <summary>
        /// Gets the object to delete.
        /// </summary>
        public TEntity Delete { get; }

        /// <summary>
        /// Gets the claims principal user.
        /// </summary>
        public ClaimsPrincipal User { get; }
    }
}