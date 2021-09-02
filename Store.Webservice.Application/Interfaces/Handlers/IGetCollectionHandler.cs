using System.Collections.Generic;
using MediatR;
using Store.Webservice.Application.Requests;

namespace Store.Webservice.Application.Interfaces.Handlers
{
    /// <summary>
    /// Handles the retrieval of the collection of specified type objects.
    /// </summary>
    /// <typeparam name="TRequest">Request object.</typeparam>
    /// <typeparam name="TResponse">Response object.</typeparam>
    public interface
        IGetCollectionHandler<TRequest, TResponse> : IRequestHandler<GetCollectionRequest<TRequest, TResponse>,
            IEnumerable<TResponse>>
    {
    }
}