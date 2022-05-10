using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Rentas.Renta
{
    public class ActualizarViewModel
    {
        [Required]
        public int id_renta { get; set; }
        [Required(ErrorMessage = "El id_cliente es obligatorio.")]
        public int id_cliente { get; set; }
        [Required(ErrorMessage = "La id_empleado es obligatorio.")]
        public int id_empleado { get; set; }
        [Required(ErrorMessage = "La fecha es obligatorio.")]
        public DateTime fecha { get; set; }
        public List<DetalleModel> detalle { get; set; }
    }
}
