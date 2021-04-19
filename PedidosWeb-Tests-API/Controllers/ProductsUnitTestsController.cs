using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PedidosWeb_API.Controllers;
using PedidosWeb_API.Data;
using PedidosWeb_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PedidosWeb_Tests_API.Controllers
{
    public class ProductsUnitTestsController
    {
        private readonly IUnitOfWork _uow;
        public static DbContextOptions<PedidosWebContext> options;
        private static PedidosWebContext _context;
        private static string _connectionString = "Data Source=.\\SQLEXPRESS;Database=PedidosWebDB_new;Integrated Security=true;";

        static ProductsUnitTestsController()
        {
            options = new DbContextOptionsBuilder<PedidosWebContext>().UseSqlServer(_connectionString).Options;
            _context = new PedidosWebContext(options);
        }

        public ProductsUnitTestsController()
        {
            _uow = new UnitOfWork(_context);
        }

        [Fact]
        public void GetProducts_OkResult()
        {
            //Arrange
            var controller = new ProductsController(_uow);

            //Act
            var result = controller.Get();

            //Assert
            Assert.IsType<List<Product>>(result.Value);
        }

        [Fact]
        public void GetProductById_OkResult()
        {
            //Arrange
            var controller = new ProductsController(_uow);
            int productId = 2;

            //Act
            var result = controller.Get(productId);

            //Assert
            Assert.IsType<Product>(result.Value);
        }

        [Fact]
        public void GetProductById_NotFoundResult()
        {
            //Arrange
            var controller = new ProductsController(_uow);
            int productId = 3333333;

            //Act
            var result = controller.Get(productId);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void CreateProduct_OkResult()
        {
            //Arrange
            var controller = new ProductsController(_uow);
            var product = new Product
            {
                Name = "Produto Teste",
                Price = 1000,
                CategoryId = 2
            };

            //Act
            var result = controller.Post(product);
            var created = controller.Get(product.ProductId);
            var createdProduct = created.Value.Should().BeAssignableTo<Product>().Subject;

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
            Assert.Equal(createdProduct.Name, product.Name);
            Assert.Equal(createdProduct.Price, product.Price);
        }

        [Fact]
        public void CreateProduct_BadResult()
        {
            //Arrange
            var controller = new ProductsController(_uow);
            var product = new Product
            {
                Name = "Produto Teste",
                Price = 1000
            };

            //Act
            var result = controller.Post(product);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void UpdateProduct_OkResult()
        {
            //Arrange
            var controller = new ProductsController(_uow);
            var productId = 2;

            var product = new Product
            {
                ProductId = productId,
                Name = "Produto Teste Update",
                Price = 1500,
                CategoryId = 8
            };

            //Act
            var result = controller.Put(productId, product);
            var updated = controller.Get(product.ProductId);
            var updatedProduct = updated.Value.Should().BeAssignableTo<Product>().Subject;

            //Assert
            Assert.IsType<OkResult>(result.Result);
            Assert.Equal(updatedProduct.ProductId, product.ProductId);
            Assert.Equal(updatedProduct.Name, product.Name);
        }

        [Fact]
        public void UpdateProduct_BadResult()
        {
            //Arrange
            var controller = new ProductsController(_uow);
            var productId = 2;

            var product = new Product
            {
                ProductId = 3,
                Name = "Produto Teste Update"

            };

            //Act
            var result = controller.Put(productId, product);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void DeleteProduct_OkResult()
        {
            //Arrange
            var controller = new ProductsController(_uow);

            //Act
            var result = controller.Delete(18);

            //Assert
            Assert.IsType<OkResult>(result.Result);
        }
    }
}
