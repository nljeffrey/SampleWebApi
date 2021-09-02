using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Store.Webservice.Application.DTOs.Responses;
using Store.Webservice.Application.Interfaces.Handlers;
using Store.Webservice.Application.Interfaces.Repository;
using Store.Webservice.Application.Requests;
using Store.Webservice.Application.Resources;
using Store.Webservice.Domain.Entities;

namespace Store.Webservice.Application.Handlers.ProductHandlers
{
    /// <inheritdoc />
    public class GetProductByIdHandler : IGetSingleHandler<int, ProductItemResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductByIdHandler"/> class.
        /// </summary>
        /// <param name="unitOfWork">Unit of work object.</param>
        /// <param name="mapper">Mapper object.</param>
        public GetProductByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the retrieval of a single product.
        /// </summary>
        /// <param name="request">Get single request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>ProductItemResponse response.</returns>
        public async Task<ProductItemResponse> Handle(GetSingleRequest<int, ProductItemResponse> request,
            CancellationToken cancellationToken)
        {
            IRepositoryAsync<Product> repository = _unitOfWork.GetRepositoryAsync<Product>();
            Product productEntity = await repository.SingleAsync(r => r.ProductId == request.Body, cancellationToken: cancellationToken);

            if (productEntity == null)
            {
                throw new KeyNotFoundException(
                    $"{nameof(Product.ProductId)} {ResourceUtils.Get(Constants.Resources.DoesNotExist)}");
            }

            return _mapper.Map<ProductItemResponse>(productEntity);
        }
    }
}