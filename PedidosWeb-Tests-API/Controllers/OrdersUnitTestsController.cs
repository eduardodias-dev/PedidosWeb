using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PedidosWeb_API.Controllers;
using PedidosWeb_API.Data;
using PedidosWeb_API.Models;
using PedidosWeb_API.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PedidosWeb_Tests_API.Controllers
{
    public class OrdersUnitTestsController
    {
        private readonly IUnitOfWork _uow;
        public static DbContextOptions<PedidosWebContext> options;
        private static PedidosWebContext _context;
        private static string _connectionString = "Data Source=.\\SQLEXPRESS;Database=PedidosWebDB_new;Integrated Security=true;";

        static OrdersUnitTestsController()
        {
            options = new DbContextOptionsBuilder<PedidosWebContext>().UseSqlServer(_connectionString).Options;
            _context = new PedidosWebContext(options);
        }

        public OrdersUnitTestsController()
        {
            _uow = new UnitOfWork(_context);
        }

        [Fact]
        public void GetOrders_OkResult()
        {
            //Arrange
            var controller = new OrdersController(_uow);

            //Act
            var result = controller.Get();

            //Assert
            Assert.IsType<List<Order>>(result.Value);
        }

        [Fact]
        public void GetOrderById_OkResult()
        {
            //Arrange
            var controller = new OrdersController(_uow);
            int orderId = 3;

            //Act
            var result = controller.Get(orderId);

            //Assert
            Assert.IsType<Order>(result.Value);
        }

        [Fact]
        public void GetOrderById_NotFoundResult()
        {
            //Arrange
            var controller = new OrdersController(_uow);
            int orderId = 3333333;

            //Act
            var result = controller.Get(orderId);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void CreateOrder_OkResult()
        {
            //Arrange
            var controller = new OrdersController(_uow);
            var order = new Order
            {
                ClientId = 2,
                OrderDate = DateTime.Now,
                Status = OrderStatus.Pending,
                DeliveryAddress = "Teste endereço"

            };

            //Act
            var result = controller.Post(order);
            var created = controller.Get(order.OrderId);
            var createdOrder = created.Value.Should().BeAssignableTo<Order>().Subject;

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
            Assert.Equal(createdOrder.ClientId, order.ClientId);
            Assert.Equal(createdOrder.OrderDate, order.OrderDate);
            Assert.Equal(createdOrder.Status, order.Status);
            Assert.Equal(createdOrder.DeliveryAddress, order.DeliveryAddress);
        }

        [Fact]
        public void UpdateOrder_OkResult()
        {
            //Arrange
            var controller = new OrdersController(_uow);
            var orderId = 3;

            var order = new Order
            {
                OrderId = orderId,
                OrderDate = DateTime.Now,
                Status = OrderStatus.Pending,
                DeliveryAddress = "Teste endereço",
                ClientId = 2

            };

            //Act
            var result = controller.Put(orderId, order);
            var updated = controller.Get(order.OrderId);
            var updatedOrder = updated.Value.Should().BeAssignableTo<Order>().Subject;

            //Assert
            Assert.IsType<OkResult>(result.Result);
            Assert.Equal(updatedOrder.ClientId, order.ClientId);
            Assert.Equal(updatedOrder.OrderDate, order.OrderDate);
            Assert.Equal(updatedOrder.Status, order.Status);
            Assert.Equal(updatedOrder.DeliveryAddress, order.DeliveryAddress);
        }

        [Fact]
        public void UpdateOrder_BadResult()
        {
            //Arrange
            var controller = new OrdersController(_uow);
            var orderId = 2;

            //Teste Faltando o cliente
            var order = new Order
            {
                OrderId = 3,
                OrderDate = DateTime.Now,
                Status = OrderStatus.Pending,
                DeliveryAddress = "Teste endereço"

            };

            //Act
            var result = controller.Put(orderId, order);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void DeleteOrder_OkResult()
        {
            //Arrange
            var controller = new OrdersController(_uow);

            //Act
            var result = controller.Delete(7);

            //Assert
            Assert.IsType<OkResult>(result.Result);
        }
    }
}
