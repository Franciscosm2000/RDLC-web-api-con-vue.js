using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Entidades.Reportes
{
    public class ReportModel
    {
        public int id_renta { get; set; }
        public string Cliente { get; set; }
        public string Empleado { get; set; }
        public string fecha_ini { get; set; }
        public string fecha_fin { get; set; }
        public string fecha { get; set; }
        public double Costo_dia { get; set; }
        public int Cantidad_dia { get; set; }
        public double Total { get; set; }
    }
}
