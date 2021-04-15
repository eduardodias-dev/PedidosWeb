using PedidosWeb_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PedidosWeb_API.Data.Repository
{
    public class ClientRepository: Repository<Client>, IRepository<Client>
    {
        public ClientRepository(PedidosWebContext context) : base(context) { }
}
}
