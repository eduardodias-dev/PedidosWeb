using PedidosWeb_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PedidosWeb_API.Data.Repository
{
    public class CategoryRepository: Repository<Category>, IRepository<Category>
    {
        public CategoryRepository(PedidosWebContext context) : base(context) { }
    }
}
