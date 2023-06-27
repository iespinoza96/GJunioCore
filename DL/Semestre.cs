using System;
using System.Collections.Generic;

namespace DL;

public partial class Semestre
{
    public byte IdSemestre { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Materium> Materia { get; set; } = new List<Materium>();
}
