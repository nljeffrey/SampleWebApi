using System.Security.Claims;
using MediatR;

namespace Store.Webservice.Application.Requests
{
    /// <summary>
    /// Generic request to create a new object.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    /// <typeparam name="TResponse">Response type.</typeparam>
    public class CreateRequest<TEntity, TResponse> : IRequest<TResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateRequest{TEntity, TResponse}"/> class.
        /// </summary>
        /// <param name="item">The item to create.</param>
        /// <param name="user">Claims principal user.</param>
        public CreateRequest(TEntity item, ClaimsPrincipal user = null)
        {
            Item = item;
            User = user;
        }

        /// <summary>
        /// Gets the item to create.
        /// </summary>
        public TEntity Item { get; }

        /// <summary>
        /// Gets the claims principal user.
        /// </summary>
        public ClaimsPrincipal User { get; }
    }
}