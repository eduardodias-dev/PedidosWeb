using PedidosWeb_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PedidosWeb_API.Data.Repository
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        IQueryable<OrderItem> GetOrderItemsAndOrder();
        OrderItem GetByIdAndIncludeOrder(Expression<Func<OrderItem, bool>> expression);
    }
}
