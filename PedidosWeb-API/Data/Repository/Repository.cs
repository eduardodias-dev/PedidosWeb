using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PedidosWeb_API.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly PedidosWebContext _context;
        public Repository(PedidosWebContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public T GetById(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().AsNoTracking().FirstOrDefault(expression);
        }

        public IQueryable<T> Get()
        {
            return _context.Set<T>().AsNoTracking().AsQueryable();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
