using Sistema.Entidades.Rentas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sistema.Entidades.Persona
{
    public class Empleado
    {
        [Required(ErrorMessage = "El id de registro es obligatorio.")]
        public int id_empleado { get; set; }
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
        public bool estado { get; set; }

        #region RELACIONES
        public ICollection<Renta> renta { get; set; }
        #endregion
    }
}
