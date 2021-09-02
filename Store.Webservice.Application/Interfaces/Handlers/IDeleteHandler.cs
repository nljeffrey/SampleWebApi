using MediatR;
using Store.Webservice.Application.Requests;

namespace Store.Webservice.Application.Interfaces.Handlers
{
    /// <summary>
    /// Handles the deletion of the specified entity type object.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    public interface IDeleteHandler<TEntity> : IRequestHandler<DeleteRequest<TEntity>>
    {
    }
}