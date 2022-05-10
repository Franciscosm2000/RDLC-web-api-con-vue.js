using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Vehiculos.Categoria
{
    public class ActualizarViewModel
    {
        [Required(ErrorMessage = "El id de registro es obligatorio.")]
        public int id_categoria { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string nombre { get; set; }
        //[Required(ErrorMessage = "La descripcion es obligatorio.")]
        public string descripcion { get; set; }
        [Required(ErrorMessage = "El usuario es obligatorio.")]
        public string usuario { get; set; }
    }
}
