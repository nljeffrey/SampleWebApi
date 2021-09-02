using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Store.Webservice.Application.DTOs.Responses;
using Store.Webservice.Application.Requests;
using Store.Webservice.WebService.Controllers;

namespace Store.Webservice.WebService.Tests.Controllers
{
    [TestClass]
    public class ProductControllerTests
    {
        private readonly ILogger<ProductController> _logger;

        public ProductControllerTests()
        {
            _logger = Mock.Of<ILogger<ProductController>>();
        }

        [TestMethod]
        public async Task GetProductMustReturnOk()
        {
            // Arrange
            const int productId = 1;
            const string productName = "Product";

            var getProductResponse = new ProductItemResponse
            {
                Id = productId,
                Name = productName
            };

            var mediator = Mock.Of<IMediator>(m =>
                m.Send(It.IsAny<GetSingleRequest<int, ProductItemResponse>>(), It.IsAny<CancellationToken>()) ==
                Task.FromResult(getProductResponse));

            var controller = new ProductController(_logger, mediator);

            // Act
            IActionResult result = await controller.GetProduct(productId, CancellationToken.None);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var actualResult = (result as OkObjectResult)?.Value as ProductItemResponse;

            Assert.AreEqual(productId, actualResult?.Id);
            Assert.AreEqual(productName, actualResult?.Name);
        }
    }
}