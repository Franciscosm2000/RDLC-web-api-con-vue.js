using Sistema.Entidades.Rentas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sistema.Entidades.Vehiculos
{
    public class Vehiculo
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
        public double Costo_dia { get; set; }
        [Required(ErrorMessage = "El estado es obligatorio.")]
        public bool estado { get; set; }


        #region RELACIONES
        public ICollection<Detalle_Renta> detalle_renta { get; set; }
        public Modelo_Vehiculo modelo_vehiculo { get; set; }
        #endregion
    }
}
