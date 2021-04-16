using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PedidosWeb_API.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
    }
}
