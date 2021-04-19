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
    public class ClientsUnitTestsController
    {
        private readonly IUnitOfWork _uow;
        public static DbContextOptions<PedidosWebContext> options;
        private static PedidosWebContext _context;
        private static string _connectionString = "Data Source=.\\SQLEXPRESS;Database=PedidosWebDB_new;Integrated Security=true;";

        static ClientsUnitTestsController()
        {
            options = new DbContextOptionsBuilder<PedidosWebContext>().UseSqlServer(_connectionString).Options;
            _context = new PedidosWebContext(options);
        }

        public ClientsUnitTestsController()
        {
            _uow = new UnitOfWork(_context);
        }

        [Fact]
        public void GetClients_OkResult()
        {
            //Arrange
            var controller = new ClientsController(_uow);

            //Act
            var result = controller.Get();

            //Assert
            Assert.IsType<List<Client>>(result.Value);
        }

        [Fact]
        public void GetClientById_OkResult()
        {
            //Arrange
            var controller = new ClientsController(_uow);
            int clientId = 2;

            //Act
            var result = controller.Get(clientId);

            //Assert
            Assert.IsType<Client>(result.Value);
        }

        [Fact]
        public void GetClientById_NotFoundResult()
        {
            //Arrange
            var controller = new ClientsController(_uow);
            int clientId = 3333333;

            //Act
            var result = controller.Get(clientId);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void CreateClient_OkResult()
        {
            //Arrange
            var controller = new ClientsController(_uow);
            var client = new Client
            {
                Name = "Cliente Teste",
                Address = "Endereço Teste"

            };

            //Act
            var result = controller.Post(client);
            var created = controller.Get(client.ClientId);
            var createdClient = created.Value.Should().BeAssignableTo<Client>().Subject;

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
            Assert.Equal(createdClient.Name, client.Name);
            Assert.Equal(createdClient.Address, client.Address);

        }

        [Fact]
        public void UpdateClient_OkResult()
        {
            //Arrange
            var controller = new ClientsController(_uow);
            var clientId = 2;

            var client = new Client
            {
                ClientId = clientId,
                Name = "Cliente Teste Update",
                Address = "Endereço Teste"

            };

            //Act
            var result = controller.Put(clientId, client);
            var updated = controller.Get(client.ClientId);
            var updatedClient = updated.Value.Should().BeAssignableTo<Client>().Subject;

            //Assert
            Assert.IsType<OkResult>(result.Result);
            Assert.Equal(updatedClient.ClientId, client.ClientId);
            Assert.Equal(updatedClient.Name, client.Name);
            Assert.Equal(updatedClient.Address, client.Address);
        }

        [Fact]
        public void UpdateClient_BadResult()
        {
            //Arrange
            var controller = new ClientsController(_uow);
            var clientId = 2;

            var client = new Client
            {
                ClientId = 3,
                Name = "Cliente Teste Update"

            };

            //Act
            var result = controller.Put(clientId, client);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void DeleteClient_OkResult()
        {
            //Arrange
            var controller = new ClientsController(_uow);

            //Act
            var result = controller.Delete(14);

            //Assert
            Assert.IsType<OkResult>(result.Result);
        }
    }
}
