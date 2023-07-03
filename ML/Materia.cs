using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ML
{
    public class Materia
    {
        public int IdMateria { get; set; } //va permitir almacenar el null

        [Required]
        [DisplayName ("Nombre: ")]
        public string Nombre { get; set; }

        [RegularExpression(@"^[0-9]+$",
         ErrorMessage = "Solo se aceptan números.")]
        public byte Creditos { get; set; }
        public ML.Semestre Semestre { get; set; } // propiedad de navegacion fk
        public ML.Horario Horario { get; set; }

        public string Imagen { get; set; }

        public string FechaCreacion { get; set; }
        public List<object> Materias { get; set; }

    }
}
