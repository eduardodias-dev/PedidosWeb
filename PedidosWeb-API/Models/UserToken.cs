using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PedidosWeb_API.Models
{
    public class UserToken
    {
        public bool Authenticated { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
        public DateTime Expiration { get; set; }
    }
}
