using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PedidosWeb_API.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId{get;set;}
        public int OrderId{get;set;}
        public Order Order{get;set;}
        public int ProductId { get; set; }
        public Product Product{get;set;}
        public int Quantity{ get; set; }
        
    }
}