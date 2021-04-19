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
    public class OrderItemsUnitTestsController
    {
        private readonly IUnitOfWork _uow;
        public static DbContextOptions<PedidosWebContext> options;
        private static PedidosWebContext _context;
        private static string _connectionString = "Data Source=.\\SQLEXPRESS;Database=PedidosWebDB_new;Integrated Security=true;";

        static OrderItemsUnitTestsController()
        {
            options = new DbContextOptionsBuilder<PedidosWebContext>().UseSqlServer(_connectionString).Options;
            _context = new PedidosWebContext(options);
        }

        public OrderItemsUnitTestsController()
        {
            _uow = new UnitOfWork(_context);
        }

        [Fact]
        public void GetOrderItems_OkResult()
        {
            //Arrange
            var controller = new OrderItemsController(_uow);

            //Act
            var result = controller.Get();

            //Assert
            Assert.IsType<List<OrderItem>>(result.Value);
        }

        [Fact]
        public void GetOrderItemById_OkResult()
        {
            //Arrange
            var controller = new OrderItemsController(_uow);
            int orderItemId = 1;

            //Act
            var result = controller.Get(orderItemId);

            //Assert
            Assert.IsType<OrderItem>(result.Value);
        }

        [Fact]
        public void GetOrderItemById_NotFoundResult()
        {
            //Arrange
            var controller = new OrderItemsController(_uow);
            int orderId = 3333333;

            //Act
            var result = controller.Get(orderId);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void CreateOrderItem_OkResult()
        {
            //Arrange
            var controller = new OrderItemsController(_uow);
            var orderItem = new OrderItem
            {
                OrderId = 3,
                ProductId = 2,
                Quantity = 5

            };

            //Act
            var result = controller.Post(orderItem);
            var created = controller.Get(orderItem.OrderItemId);
            var createdOrder = created.Value.Should().BeAssignableTo<OrderItem>().Subject;

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
            Assert.Equal(createdOrder.OrderItemId, orderItem.OrderItemId);
            Assert.Equal(createdOrder.OrderId, orderItem.OrderId);
            Assert.Equal(createdOrder.ProductId, orderItem.ProductId);
            Assert.Equal(createdOrder.Quantity, orderItem.Quantity);
        }

        [Fact]
        public void UpdateOrder_OkResult()
        {
            //Arrange
            var controller = new OrderItemsController(_uow);
            var orderItemId = 2;

            var orderItem = new OrderItem
            {
                OrderItemId = orderItemId,
                OrderId = 3,
                ProductId = 2,
                Quantity = 4

            };

            //Act
            var result = controller.Put(orderItemId, orderItem);
            var updated = controller.Get(orderItem.OrderItemId);
            var updatedOrder = updated.Value.Should().BeAssignableTo<OrderItem>().Subject;

            //Assert
            Assert.IsType<OkResult>(result.Result);
            Assert.Equal(updatedOrder.OrderItemId, orderItem.OrderItemId);
            Assert.Equal(updatedOrder.OrderId, orderItem.OrderId);
            Assert.Equal(updatedOrder.ProductId, orderItem.ProductId);
            Assert.Equal(updatedOrder.Quantity, orderItem.Quantity);
        }

        [Fact]
        public void UpdateOrderItem_BadResult()
        {
            //Arrange
            var controller = new OrderItemsController(_uow);
            var orderItemId = 2;

            //Teste Faltando o Pedido
            var orderItem = new OrderItem
            {
                OrderItemId = orderItemId,
                ProductId = 2,
                Quantity = 4

            };

            //Act
            var result = controller.Put(orderItemId, orderItem);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void DeleteOrderItem_OkResult()
        {
            //Arrange
            var controller = new OrderItemsController(_uow);

            //Act
            var result = controller.Delete(10);

            //Assert
            Assert.IsType<OkResult>(result.Result);
        }
    }
}
