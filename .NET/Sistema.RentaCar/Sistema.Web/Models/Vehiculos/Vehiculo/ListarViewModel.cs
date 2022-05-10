using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Vehiculos.Vehiculo
{
    public class ListarViewModel
    {
        public int id_vehiculo { get; set; }
        public int id_modelo { get; set; }
        public string modelo { get; set; }
        public string placa { get; set; }
        public string no_Chasis { get; set; }
        public string motor { get; set; }
        public string color { get; set; }
        public double Costo_dia { get; set; }
        public bool estado { get; set; }
    }
}
