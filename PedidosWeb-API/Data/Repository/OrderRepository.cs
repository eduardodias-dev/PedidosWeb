using Microsoft.EntityFrameworkCore;
using PedidosWeb_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PedidosWeb_API.Data.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(PedidosWebContext context): base(context){}

        public Order GetByIdAndIncludeClient(Expression<Func<Order, bool>> expression)
        {
            return _context.Orders.AsNoTracking().Include(x => x.Items).Include(x => x.Client).FirstOrDefault(expression);
        }

        public IQueryable<Order> GetOrderAndClients()
        {
            return _context.Orders.AsNoTracking().Include(x => x.Items).Include(order => order.Client);
        }

    }
}
