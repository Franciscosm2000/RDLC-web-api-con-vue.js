using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Personas.Empleados
{
    public class CrearViewModel
    {
        [Required(ErrorMessage = "El identificacion es obligatorio.")]
        public string cedula { get; set; }
        [Required(ErrorMessage = "El primer_nombre es obligatorio.")]
        public string primer_nombre { get; set; }
        public string segundo_nombre { get; set; }
        [Required(ErrorMessage = "El primer_apellido es obligatorio.")]
        public string primer_apellido { get; set; }
        public string segundo_apellido { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
    }
}
