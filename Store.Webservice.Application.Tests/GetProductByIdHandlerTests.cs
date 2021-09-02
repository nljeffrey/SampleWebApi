using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Store.Webservice.Application.DTOs.Responses;
using Store.Webservice.Application.Handlers.ProductHandlers;
using Store.Webservice.Application.Interfaces.Repository;
using Store.Webservice.Application.Requests;
using Store.Webservice.Domain.Entities;
using Store.Webservice.WebService.Infrastructure.Automapper;

namespace Store.Webservice.Application.Tests
{
    [TestClass]
    public class GetProductByIdHandlerTests
    {
        private readonly Mapper _testMapper;

        public GetProductByIdHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            _testMapper = new Mapper(mapperConfig);
        }

        [TestMethod]
        public async Task CanGetProduct()
        {
            // Arrange
            var productEntry = new Product
            {
                ProductId = 1,
                ProductName = "Product 1"
            };

            var repository = Mock.Of<IRepositoryAsync<Product>>(r =>
                r.SingleAsync(It.IsAny<Expression<Func<Product, bool>>>(), null, null, true,
                    It.IsAny<CancellationToken>()) == Task.FromResult(productEntry));

            var unitOfWork = Mock.Of<IUnitOfWork>(u => u.GetRepositoryAsync<Product>() == repository);

            // Act
            var handler = new GetProductByIdHandler(unitOfWork, _testMapper);
            var response =
                await handler.Handle(new GetSingleRequest<int, ProductItemResponse>(1), CancellationToken.None);

            // Assert
            Assert.AreEqual(productEntry.ProductId, response.Id);
            Assert.AreEqual(productEntry.ProductName, response.Name);
        }
    }
}