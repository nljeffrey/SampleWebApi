using MediatR;
using Store.Webservice.Application.Requests;

namespace Store.Webservice.Application.Interfaces.Handlers
{
    /// <summary>
    /// Handles the creation of the specified entity type object.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    /// <typeparam name="TResponse">Response type.</typeparam>
    public interface ICreateHandler<TEntity, TResponse> : IRequestHandler<CreateRequest<TEntity, TResponse>, TResponse>
    {
    }
}
