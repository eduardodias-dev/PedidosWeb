using System;
using System.ComponentModel.DataAnnotations;

namespace PedidosWeb_API.Models
{
    public class Product
    {
        [Key]
        public int ProductId{get;set;}
        [Required]
        public string Name{get;set;}
        public double Price{get;set;}
        public int CategoryId{get;set;}
        public Category Category{get;set;}

        public Product()
        {
            
        }
        
    }
}