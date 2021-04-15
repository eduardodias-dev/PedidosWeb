using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PedidosWeb_API.Models
{
    public class Category
    {
        [Key]
        public int CategoryId{get;set;}
        [Required]
        [MaxLength(50)]
        public string Name{get;set;}
        public List<Product> Products{get;set;}

        public Category()
        {
            Products = new List<Product>();
        }
       
    }
}