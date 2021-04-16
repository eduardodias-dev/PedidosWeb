using PedidosWeb_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PedidosWeb_API.Data.Repository
{
    public interface IOrderRepository : IRepository<Order>
    {
        IQueryable<Order> GetOrderAndClients();
        Order GetByIdAndIncludeClient(Expression<Func<Order, bool>> expression);
    }
}
