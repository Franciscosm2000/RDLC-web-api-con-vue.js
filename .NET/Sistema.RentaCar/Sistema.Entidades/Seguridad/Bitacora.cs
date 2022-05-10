using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sistema.Entidades.Seguridad
{
    public class Bitacora
    {
        [Required]
        public int? id { get; set; }
        [Required]
        public string accion { get; set; }
        [Required]
        public string controlador { get; set; }
        [Required]
        public DateTime? fecha { get; set; }
        [Required]
        public string usuario { get; set; }
        [Required]
        public string descripcion { get; set; }
    }
}
