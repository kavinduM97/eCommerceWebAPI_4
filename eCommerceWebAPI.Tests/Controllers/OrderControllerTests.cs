using AutoFixture;
using eCommerceWebAPI.Controllers;
using eCommerceWebAPI.ErrorHandler;
using eCommerceWebAPI.Requests;
using eCommerceWebAPI.Services.Order;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace eCommerceWebAPI.Tests.Controllers

{
    public class OrderControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock <IOrderRepository> _serviceMock;
        private readonly OrderController _sut;

        public OrderControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IOrderRepository>>();
            _sut = new OrderController(_serviceMock.Object);
        }

        [Fact]
        public void PlaceOrder_ShouldReturnOkResponse_WhenOrderPlaceSuccessfully()
        {
            //Arrange


            OrderRequest request = null;


           var responseMock = _fixture.Build<OrderErrorHandler>().With(p => p.State, true).Create();

            _serviceMock.Setup(x => x.PlaceOrder(10, request)).Returns(responseMock);

            //Act
            var result = _sut.PlaceOrders(10, request);

            OkObjectResult okResult = result as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.GetType().Should().Be(typeof(OkObjectResult));
            Assert.Equal(200, okResult.StatusCode);
            _serviceMock.Verify(x => x.PlaceOrder(10, request), Times.Once);

         
        }



        [Fact]
        public void PlaceOrder_ShouldReturnBadRequestResponse_WhenOrderPlaceFaild()
        {
            //Arrange



            var request = new OrderRequest
            {
                quantity = -1,
                email = null,
            };

            var responseMock = _fixture.Create<OrderErrorHandler>();

            _serviceMock.Setup(x => x.PlaceOrder(10, request)).Returns(responseMock);

            //Act
            var result = _sut.PlaceOrders(10, request);

            BadRequestObjectResult badResult = result as BadRequestObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();
            result.GetType().Should().Be(typeof(BadRequestObjectResult));
            Assert.Equal(400, badResult.StatusCode);
            _serviceMock.Verify(x => x.PlaceOrder(10, request), Times.Once);


    
        }

        


    }
}