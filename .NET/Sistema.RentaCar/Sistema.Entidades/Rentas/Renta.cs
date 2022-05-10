using Sistema.Entidades.Persona;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sistema.Entidades.Rentas
{
    public class Renta
    {
        [Required(ErrorMessage = "El id_renta de registro es obligatorio.")]
        public int id_renta { get; set; }
        [Required(ErrorMessage = "El id_cliente es obligatorio.")]
        public int id_cliente { get; set; }
        [Required(ErrorMessage = "La id_empleado es obligatorio.")]
        public int id_empleado { get; set; }
        [Required(ErrorMessage = "La fecha es obligatorio.")]
        public DateTime fecha { get; set; }
        [Required(ErrorMessage = "El estado es obligatorio.")]
        public bool estado { get; set; }


        #region RELACIONES
        public ICollection<Detalle_Renta> detalle_renta { get; set; }
        public Cliente cliente { get; set; }
        public Empleado empleado { get; set; }
        #endregion
    }
}
