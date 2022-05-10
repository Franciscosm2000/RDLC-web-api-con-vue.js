using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Personas.Clientes
{
    public class SelectViewModel
    {
        public int id_cliente { get; set; }
        public string cedula { get; set; }
        public string nombre { get; set; }
    }
}
