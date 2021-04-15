using Microsoft.EntityFrameworkCore;
using PedidosWeb_API.Models;

namespace PedidosWeb_API.Data
{
    public class PedidosWebContext : DbContext
    {
        public PedidosWebContext(DbContextOptions<PedidosWebContext> options):base(options)
        {
            
        }
        
        public DbSet<Client> Clients{get;set;}
        public DbSet<Order> Orders{get;set;}
        public DbSet<OrderItem> OrderItems{get;set;}
        public DbSet<Product> Products{get;set;}
        public DbSet<Category> Categories{get;set;}
    }
}