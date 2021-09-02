using MediatR;
using Store.Webservice.Application.Requests;

namespace Store.Webservice.Application.Interfaces.Handlers
{
    /// <summary>
    /// Handles the update of the specified entity type object.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    public interface IUpdateHandler<TEntity> : IRequestHandler<UpdateRequest<TEntity>>
    {
    }
}
