using PedidosWeb_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PedidosWeb_API.Data.Repository
{
    public class ProductRepository: Repository<Product>, IRepository<Product>
    {
        public ProductRepository(PedidosWebContext context) : base(context) { }
}
}
