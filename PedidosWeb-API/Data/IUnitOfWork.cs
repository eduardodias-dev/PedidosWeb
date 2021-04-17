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
        IRepository<Category> CategoryRepository{ get; }
        IRepository<Client> ClientRepository { get; }
        IOrderRepository OrderRepository { get; }
        IProductRepository ProductRepository { get; }
        IOrderItemRepository OrderItemRepository { get; }
        void Commit();
    }
}
