using MediatR;
using Store.Webservice.Application.Requests;

namespace Store.Webservice.Application.Interfaces.Handlers
{
    /// <summary>
    /// Handles the retrieval of the single type object.
    /// </summary>
    /// <typeparam name="TRequest">Request type.</typeparam>
    /// <typeparam name="TResponse">Response type.</typeparam>
    public interface
        IGetSingleHandler<TRequest, TResponse> : IRequestHandler<GetSingleRequest<TRequest, TResponse>, TResponse>
    {
    }
}