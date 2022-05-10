using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Rentas.Detalle
{
    public class DetalleViewModel
    {
        public int id_detalle_renta { get; set; }
        public int id_renta { get; set; }
        public int id_vehiculo { get; set; }
        public string vehiculo { get; set; }
        public double monto_dia { get; set; }
        public int num_dia { get; set; }
        public DateTime fecha_ini { get; set; }
        public DateTime fecha_fin { get; set; }
        public double costo_dia { get; set; }
    }
}
