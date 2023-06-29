using System;
using System.Collections.Generic;

namespace DL;

public partial class Materium
{
    public int IdMateria { get; set; }

    public string? Nombre { get; set; }

    public byte? Creditos { get; set; }

    public byte? IdSemestre { get; set; }

    public string? Imagen { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<Horario> Horarios { get; set; } = new List<Horario>();

    public virtual Semestre? IdSemestreNavigation { get; set; }
    public string NombreSemestre { get; set; }
    public string NombreGrupo { get; set; }
    public string NombrePlantel { get; set; }
}
