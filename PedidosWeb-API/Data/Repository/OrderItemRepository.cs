using Microsoft.EntityFrameworkCore;
using PedidosWeb_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PedidosWeb_API.Data.Repository
{
    public class OrderItemRepository: Repository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(PedidosWebContext context) : base(context) { }

        public OrderItem GetByIdAndIncludeOrder(Expression<Func<OrderItem, bool>> expression)
        {
            return _context.OrderItems.Include(x => x.Order).FirstOrDefault(expression);
        }

        public IQueryable<OrderItem> GetOrderItemsAndOrder()
        {
            return _context.OrderItems.Include(x => x.Order);
        }
    }
}
