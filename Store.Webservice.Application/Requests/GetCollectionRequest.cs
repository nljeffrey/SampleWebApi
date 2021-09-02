using System.Collections.Generic;
using System.Security.Claims;
using MediatR;

namespace Store.Webservice.Application.Requests
{
    /// <summary>
    /// Generic request to get a collection.
    /// </summary>
    /// <typeparam name="TRequest">Request object.</typeparam>
    /// <typeparam name="TResponse">Response object.</typeparam>
    public class GetCollectionRequest<TRequest, TResponse> : IRequest<IEnumerable<TResponse>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCollectionRequest{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="request">Request object.</param>
        /// <param name="user">Claims principal user.</param>
        public GetCollectionRequest(TRequest request, ClaimsPrincipal user = null)
        {
            Request = request;
            User = user;
        }

        /// <summary>
        /// Gets the request object.
        /// </summary>
        public TRequest Request { get; }

        /// <summary>
        /// Gets the claims principal user.
        /// </summary>
        public ClaimsPrincipal User { get; }
    }
}