using System.Security.Claims;
using MediatR;

namespace Store.Webservice.Application.Requests
{
    /// <summary>
    /// Generic request to get a single object.
    /// </summary>
    /// <typeparam name="TRequest">Request type.</typeparam>
    /// <typeparam name="TResponse">Response type.</typeparam>
    public class GetSingleRequest<TRequest, TResponse> : IRequest<TResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSingleRequest{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="request">Request object.</param>
        /// <param name="user">Claims principal user.</param>
        public GetSingleRequest(TRequest data, ClaimsPrincipal user = null)
        {
            Body = data;
            User = user;
        }

        /// <summary>
        /// Gets the request object.
        /// </summary>
        public TRequest Body { get; }

        /// <summary>
        /// Gets the claims principal user.
        /// </summary>
        public ClaimsPrincipal User { get; }
    }
}