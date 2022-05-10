using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Vehiculos.Modelo_Vehiculo
{
    public class SelectViewModel
    {
        public int id_modelo { get; set; }
        public int id_categoria { get; set; }
        public string categoria { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
    }
}
