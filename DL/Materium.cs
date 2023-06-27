using System;
using System.Collections.Generic;

namespace DL;

public partial class Materium
{
    public int IdMateria { get; set; }

    public string? Nombre { get; set; }

    public byte? Creditos { get; set; }

    public byte? IdSemestre { get; set; }

    public virtual Semestre? IdSemestreNavigation { get; set; }

    public string NombreSemestre { get; set; }
}
