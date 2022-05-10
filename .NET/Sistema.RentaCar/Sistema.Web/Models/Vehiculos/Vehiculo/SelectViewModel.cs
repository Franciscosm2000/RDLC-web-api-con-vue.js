using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Vehiculos.Vehiculo
{
    public class SelectViewModel
    {
        public int id_vehiculo { get; set; }
        public int id_modelo { get; set; }
        public string modelo { get; set; }
        public string placa { get; set; }
        public double precio_dia { get; set; }
    }
}
