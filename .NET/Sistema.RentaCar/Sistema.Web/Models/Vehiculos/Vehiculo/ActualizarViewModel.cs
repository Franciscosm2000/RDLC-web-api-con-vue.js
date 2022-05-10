using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Vehiculos.Vehiculo
{
    public class ActualizarViewModel
    {
        [Required(ErrorMessage = "El id_vehiculo de registro es obligatorio.")]
        public int id_vehiculo { get; set; }
        [Required(ErrorMessage = "El id_modelo de registro es obligatorio.")]
        public int id_modelo { get; set; }
        [Required(ErrorMessage = "El placa es obligatorio.")]
        public string placa { get; set; }
        [Required(ErrorMessage = "La no_Chasis es obligatorio.")]
        public string no_Chasis { get; set; }
        [Required(ErrorMessage = "La motor es obligatorio.")]
        public string motor { get; set; }
        [Required(ErrorMessage = "La color es obligatorio.")]
        public string color { get; set; }
        [Required(ErrorMessage = "La Costo_dia es obligatorio.")]
        public float Costo_dia { get; set; }
    }
}
