using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Rentas.Renta
{
    public class ListarViewModel
    {
        public int id_renta { get; set; }
        public int id_cliente { get; set; }
        public string cliente { get; set; }
        public int id_empleado { get; set; }
        public string empleado { get; set; }
        public DateTime fecha { get; set; }
        public bool estado { get; set; }
    }
}
