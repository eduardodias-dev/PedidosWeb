using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PedidosWeb_API.Data.Repository
{
    public interface IRepository<T> 
    {
        IQueryable<T> Get();
        T GetById(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
