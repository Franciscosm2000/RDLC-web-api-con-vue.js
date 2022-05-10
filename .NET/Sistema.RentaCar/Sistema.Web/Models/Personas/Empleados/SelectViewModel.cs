using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Personas.Empleados
{
    public class SelectViewModel
    {
        public int id_empleado { get; set; }
        public string cedula { get; set; }
        public string nombre { get; set; }
    }
}
