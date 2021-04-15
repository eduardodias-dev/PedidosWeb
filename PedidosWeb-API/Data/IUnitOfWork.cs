using PedidosWeb_API.Data.Repository;
using PedidosWeb_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PedidosWeb_API.Data
{
    public interface IUnitOfWork
    {
        IRepository<Order> OrderRepository { get; }
        IRepository<Client> ClientRepository { get; }
        IRepository<Product> ProductRepository { get; }
        IRepository<OrderItem> OrderItemRepository { get; }
        IRepository<Category> CategoryRepository{ get; }
        void Commit();
    }
}
