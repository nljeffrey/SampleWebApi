using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.Webservice.Application.DTOs.Requests;
using Store.Webservice.Application.DTOs.Responses;
using Store.Webservice.Application.Requests;

namespace Store.Webservice.WebService.Controllers
{
    /// <summary>
    /// Contains Products related endpoints.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="mediator">The mediator object.</param>
        public ProductController(
            ILogger<ProductController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Gets the product by its identifier.
        /// </summary>
        /// <param name="productId">Product identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The product item response object.</returns>
        /// <response code="200">OK.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Not Found.</response>
        /// <response code="500">Internal Server Error.</response>
        /// <example>store/product/5</example>
        [HttpGet("{productId}", Name = "GetProduct")]
        [ProducesResponseType(typeof(ProductItemResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProduct([FromRoute] int productId, CancellationToken cancellationToken)
        {
            _logger.Log(LogLevel.Debug, $"Get product by id {productId} request received.");

            ProductItemResponse response =
                await _mediator.Send(new GetSingleRequest<int, ProductItemResponse>(productId), cancellationToken);

            _logger.Log(LogLevel.Debug, $"Returning product with id {productId}.");

            return Ok(response);
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="product">AddProductRequest DTO.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The new product entity with the location header filled.</returns>
        /// <response code="201">Created.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="500">Internal Server Error.</response>
        /// <example>product/AddProduct</example>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///         "naam": "My product"
        ///     }
        /// </remarks>
        [HttpPost(Name = "AddProduct")]
        public async Task<IActionResult> CreateKlant(
            [FromBody] AddProductRequest product, CancellationToken cancellationToken)
        {
            _logger.Log(LogLevel.Debug, "Create product request received.");

            int productId = await _mediator.Send(new CreateRequest<AddProductRequest, int>(product),
                cancellationToken);

            _logger.Log(LogLevel.Debug, $"Product {productId} created.");

            return CreatedAtAction("GetProduct", new { productId }, null);
        }
    }
}