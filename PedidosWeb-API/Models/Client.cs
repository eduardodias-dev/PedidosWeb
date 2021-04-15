using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PedidosWeb_API.Models
{
    public class Client
    {
        [Key]
        public int ClientId{get;set;}
        public string Name{get;set;}
        public string Address{get;set;}
        public List<Order> Orders{get;set;}

        public Client()
        {
            Orders = new List<Order>();
        }
    }
}