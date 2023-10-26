using System;
using System.Collections.Generic;

namespace API_TrabajoFinal.Models;

public partial class Planificacion
{
    public string CodMat { get; set; } = null!;

    public string CodTurno { get; set; } = null!;

    public string Año { get; set; } = null!;

    public DateTime? FechaExamen { get; set; }

    public virtual Materia CodMatNavigation { get; set; } = null!;

    public virtual Turno CodTurnoNavigation { get; set; } = null!;
}
