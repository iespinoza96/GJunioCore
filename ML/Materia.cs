using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Materia
    {
        public int IdMateria { get; set; } //va permitir almacenar el null
        public string Nombre { get; set; }
        public byte Creditos { get; set; }
        public ML.Semestre Semestre { get; set; } // propiedad de navegacion fk
        public ML.Horario Horario { get; set; }
        public List<object> Materias { get; set; }
    }
}
