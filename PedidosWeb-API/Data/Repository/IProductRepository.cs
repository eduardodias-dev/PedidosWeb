using PedidosWeb_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PedidosWeb_API.Data.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        IQueryable<Product> GetProductsWithCategory();
        Product GetProductByIdWithCategory(Expression<Func<Product, bool>> expression);
    }
}
