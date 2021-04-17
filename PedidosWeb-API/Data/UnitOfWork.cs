using PedidosWeb_API.Data.Repository;
using PedidosWeb_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PedidosWeb_API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PedidosWebContext _context;
        public UnitOfWork(PedidosWebContext context)
        {
            _context = context;
        }
        private IRepository<Client> _clientRepo;
        private IRepository<Category> _cateogoryRepo;
        private IOrderRepository _orderRepo;
        private IProductRepository _productRepo;
        private IOrderItemRepository _orderItemRepo;

        public IOrderRepository OrderRepository
        {
            get
            {
                return _orderRepo = _orderRepo ?? new OrderRepository(_context);
            }
        }

        public IRepository<Client> ClientRepository
        {
            get
            {
                return _clientRepo = _clientRepo ?? new ClientRepository(_context);
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                return _productRepo = _productRepo ?? new ProductRepository(_context);
            }
        }

        public IOrderItemRepository OrderItemRepository
        {
            get
            {
                return _orderItemRepo = _orderItemRepo ?? new OrderItemRepository(_context);
            }
        }
        public IRepository<Category> CategoryRepository
        {
            get
            {
                return _cateogoryRepo = _cateogoryRepo ?? new CategoryRepository(_context);
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
