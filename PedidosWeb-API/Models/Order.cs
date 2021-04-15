using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PedidosWeb_API.Models.Enum;

namespace PedidosWeb_API.Models
{
    public class Order
    {
        [Key]
        public int OrderId{get;set;}
        public int ClientId { get; set; }
        public Client Client { get; set; }
        [Required]
        public DateTime OrderDate{get;set;}
        public OrderStatus Status{get;set;}
        public string DeliveryAddress{get;set;}
        public List<OrderItem> Items{get; set;}
        
        public Order()
        {
            Items = new List<OrderItem>();
        }
        

        
    }
}