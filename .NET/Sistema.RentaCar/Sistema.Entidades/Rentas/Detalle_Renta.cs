using Sistema.Entidades.Vehiculos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sistema.Entidades.Rentas
{
    public class Detalle_Renta
    {
        [Required(ErrorMessage = "El id_detalle_renta de registro es obligatorio.")]
        public int id_detalle_renta { get; set; }
        [Required(ErrorMessage = "El id_renta es obligatorio.")]
        public int id_renta { get; set; }
        [Required(ErrorMessage = "La id_vehiculo es obligatorio.")]
        public int id_vehiculo { get; set; }
        [Required(ErrorMessage = "La fecha_ini es obligatorio.")]
        public DateTime fecha_ini { get; set; }
        [Required(ErrorMessage = "La fecha_fin es obligatorio.")]
        public DateTime fecha_fin { get; set; } 

        public double costo_dia { get; set; }


        #region RELACIONES
        public Renta renta { get; set; }
        public Vehiculo vehiculo { get; set; }
        #endregion
    }
}
