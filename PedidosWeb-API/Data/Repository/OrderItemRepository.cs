using PedidosWeb_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PedidosWeb_API.Data.Repository
{
    public class OrderItemRepository: Repository<OrderItem>, IRepository<OrderItem>
    {
        public OrderItemRepository(PedidosWebContext context) : base(context) { }
    }
}
