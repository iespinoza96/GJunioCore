using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Semestre
    {
        public Semestre(byte idSemestre, string nombre)
        {
            IdSemestre = idSemestre;
            Nombre = nombre;
        }
        public Semestre()
        {

        }
        public byte IdSemestre { get; set; }
        public string Nombre { get; set; }

        public List<object> Semestres { get; set; }
    }
}
