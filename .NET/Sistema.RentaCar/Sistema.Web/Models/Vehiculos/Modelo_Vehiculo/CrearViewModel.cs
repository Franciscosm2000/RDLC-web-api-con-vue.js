using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Sistema.Web.Models.Vehiculos.Modelo_Vehiculo
{
    public class CrearViewModel
    {
        [Required(ErrorMessage = "El id_categoria es obligatorio.")]
        public int id_categoria { get; set; }
        [Required(ErrorMessage = "La marca es obligatorio.")]
        public string marca { get; set; }
        [Required(ErrorMessage = "La modelo es obligatorio.")]
        public string modelo { get; set; }
        [Required(ErrorMessage = "La combustible es obligatorio.")]
        public string combustible { get; set; }
        //[Required(ErrorMessage = "La tipo_carroceria es obligatorio.")]
        public string tipo_carroceria { get; set; }
    }
}
