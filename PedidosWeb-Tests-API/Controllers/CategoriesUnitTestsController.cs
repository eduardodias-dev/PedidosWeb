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
    public class CategoriesUnitTestsController
    {
        private readonly IUnitOfWork _uow;
        public static DbContextOptions<PedidosWebContext> options;
        private static PedidosWebContext _context;
        private static string _connectionString = "Data Source=.\\SQLEXPRESS;Database=PedidosWebDB_new;Integrated Security=true;";

        static CategoriesUnitTestsController()
        {
            options = new DbContextOptionsBuilder<PedidosWebContext>().UseSqlServer(_connectionString).Options;
            _context = new PedidosWebContext(options);
        }

        public CategoriesUnitTestsController()
        {
            _uow = new UnitOfWork(_context);
        }

        [Fact]
        public void GetCategories_OkResult()
        {
            //Arrange
            var controller = new CategoriesController(_uow);

            //Act
            var result = controller.Get();

            //Assert
            Assert.IsType<List<Category>>(result.Value);
        }

        [Fact]
        public void GetCategoryById_OkResult()
        {
            //Arrange
            var controller = new CategoriesController(_uow);
            int categoryId = 2;

            //Act
            var result = controller.Get(categoryId);

            //Assert
            Assert.IsType<Category>(result.Value);
        }

        [Fact]
        public void GetCategoryById_NotFoundResult()
        {
            //Arrange
            var controller = new CategoriesController(_uow);
            int categoryId = 3333333;

            //Act
            var result = controller.Get(categoryId);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void CreateCategory_OkResult()
        {
            //Arrange
            var controller = new CategoriesController(_uow);
            var category = new Category
            {
                Name = "Categoria Teste"

            };

            //Act
            var result = controller.Post(category);
            var created = controller.Get(category.CategoryId);
            var createdCategory = created.Value.Should().BeAssignableTo<Category>().Subject;

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
            Assert.Equal(createdCategory.Name, category.Name);
        }

        [Fact]
        public void UpdateCategory_OkResult()
        {
            //Arrange
            var controller = new CategoriesController(_uow);
            var categoryId = 2;

            var category = new Category
            {
                CategoryId = categoryId,
                Name = "Categoria Teste Update"

            };

            //Act
            var result = controller.Put(categoryId, category);
            var updated = controller.Get(category.CategoryId);
            var updatedCategory = updated.Value.Should().BeAssignableTo<Category>().Subject;

            //Assert
            Assert.IsType<OkResult>(result.Result);
            Assert.Equal(updatedCategory.CategoryId, category.CategoryId);
            Assert.Equal(updatedCategory.Name, category.Name);
        }

        [Fact]
        public void UpdateCategory_BadResult()
        {
            //Arrange
            var controller = new CategoriesController(_uow);
            var categoryId = 2;

            var category = new Category
            {
                CategoryId = 3,
                Name = "Categoria Teste Update"

            };

            //Act
            var result = controller.Put(categoryId, category);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void DeleteCategory_OkResult()
        {
            //Arrange
            var controller = new CategoriesController(_uow);
            
            //Act
            var result = controller.Delete(35);

            //Assert
            Assert.IsType<OkResult>(result.Result);
        }

    }
}
