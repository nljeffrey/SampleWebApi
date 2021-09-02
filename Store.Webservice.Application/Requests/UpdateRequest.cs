using System.Security.Claims;
using MediatR;

namespace Store.Webservice.Application.Requests
{
    /// <summary>
    /// Generic request to update an object.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    public class UpdateRequest<TEntity> : IRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRequest{TEntity}"/> class.
        /// </summary>
        /// <param name="update">Object to update.</param>
        /// <param name="user">Claims principal user.</param>
        public UpdateRequest(TEntity update, ClaimsPrincipal user = null)
        {
            Update = update;
            User = user;
        }

        /// <summary>
        /// Gets the object to update.
        /// </summary>
        public TEntity Update { get; }

        /// <summary>
        /// Gets the claims principal user.
        /// </summary>
        public ClaimsPrincipal User { get; }
    }
}