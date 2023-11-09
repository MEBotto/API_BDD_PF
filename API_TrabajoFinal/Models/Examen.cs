using System;
using System.Collections.Generic;

namespace API_TrabajoFinal.Models;

public partial class Examen
{
    public int? NroLegajoA { get; set; }

    public string CodMat { get; set; } = null!;

    public string CodTurno { get; set; } = null!;

    public string Año { get; set; } = null!;

    public int? Nota { get; set; }

    public DateTime? FechaInscripcion { get; set; }

    public virtual Materia? CodMatNavigation { get; set; }

    public virtual Turno? CodTurnoNavigation { get; set; }

    public virtual Alumno? NroLegajoANavigation { get; set; }
}
