using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Store.Webservice.Application.DTOs.Requests;
using Store.Webservice.Application.Interfaces.Handlers;
using Store.Webservice.Application.Interfaces.Repository;
using Store.Webservice.Application.Requests;
using Store.Webservice.Domain.Entities;

namespace Store.Webservice.Application.Handlers.ProductHandlers
{
    /// <inheritdoc />
    public class AddProductHandler : ICreateHandler<AddProductRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddProductHandler"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="mapper">Mapper object.</param>
        public AddProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="request">The create request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Product id of the newly created product object.</returns>
        public async Task<int> Handle(CreateRequest<AddProductRequest, int> request,
            CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var productEntity = _mapper.Map<Product>(request.Item);

            IRepositoryAsync<Product> repository = _unitOfWork.GetRepositoryAsync<Product>();
            Product newProduct = await repository.AddAsync(productEntity, cancellationToken);

            _unitOfWork.SaveChanges();

            return newProduct.ProductId;
        }
    }
}