using Microsoft.EntityFrameworkCore;
using PedidosWeb_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PedidosWeb_API.Data.Repository
{
    public class ProductRepository: Repository<Product>, IProductRepository
    {
        public ProductRepository(PedidosWebContext context) : base(context) { }

        public Product GetProductByIdWithCategory(Expression<Func<Product, bool>> expression)
        {
            return _context.Products.Include(x => x.Category).FirstOrDefault(expression);
        }

        public IQueryable<Product> GetProductsWithCategory()
        {
            return _context.Products.Include(x => x.Category);
        }
    }
}
