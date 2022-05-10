using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sistema.Entidades.Vehiculos
{
    public class Modelo_Vehiculo
    {
        [Required(ErrorMessage = "El id de registro es obligatorio.")]
        public int id_modelo { get; set; }
        [Required(ErrorMessage = "El id_categoria es obligatorio.")]
        public int id_categoria { get; set; }
        [Required(ErrorMessage = "La marca es obligatorio.")]
        public string marca { get; set; }
        [Required(ErrorMessage = "La modelo es obligatorio.")]
        public string modelo { get; set; }
        [Required(ErrorMessage = "La combustible es obligatorio.")]
        public string combustible { get; set; }
       // [Required(ErrorMessage = "La tipo_carroceria es obligatorio.")]
        public string tipo_carroceria { get; set; }
        [Required(ErrorMessage = "El estado es obligatorio.")]
        public bool estado { get; set; }


        #region RELACIONES
        public Categoria categoria { get; set; }
        public ICollection<Vehiculo> vehiculo { get; set; }
        #endregion
    }
}
