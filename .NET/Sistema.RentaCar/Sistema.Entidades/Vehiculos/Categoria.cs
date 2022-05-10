using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Xunit;

namespace Sistema.Entidades.Vehiculos
{
    public class Categoria
    {
        [Required(ErrorMessage = "El id de registro es obligatorio.")]
        public int id_categoria { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string nombre { get; set; }
       // [Required(ErrorMessage = "La descripcion es obligatorio.")]
        public string descripcion { get; set; }
        [Required(ErrorMessage = "El estado es obligatorio.")]
        public bool estado { get; set; }


        #region RELACIONES
        public ICollection< Modelo_Vehiculo> modelo_vehiculo { get; set; }
        #endregion
    }
}
