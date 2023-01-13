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



            var request = new OrderRequest
            {
                quantity = 10,
                email = "example@gmail.com",
            };

            Random rnd = new Random();
            int TrId = rnd.Next();

            var responseMock = _fixture.Create<Task<bool>>();



            _serviceMock.Setup(x => x.PlaceOrder(10, request)).Returns(responseMock);



            //Act
            var result = _sut.PlaceOrders(10, request);
          



            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<String>();
            result.GetType().Should().Be(typeof(String));
          
            _serviceMock.Verify(x => x.PlaceOrder(10, request), Times.Once);
        }



        [Fact]
        public void PlaceOrder_ShouldReturnBadRequestResponse_WhenOrderPlaceFaild()
        {
            //Arrange



            var request = new OrderRequest
            {
                quantity = 10,
                email = "example@gmail.com",
            };

            Random rnd = new Random();
            int TrId = rnd.Next();

            var responseMock = _fixture.Create<Task<bool>>();



            _serviceMock.Setup(x => x.PlaceOrder(10, request)).Returns(responseMock);



            //Act
            var result = _sut.PlaceOrders(10, request);
           



            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<String>();
            result.GetType().Should().Be(typeof(String));
           
            _serviceMock.Verify(x => x.PlaceOrder(10, request), Times.Once);
        }




    }
}