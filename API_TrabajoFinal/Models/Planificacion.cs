using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API_TrabajoFinal.Models;

public partial class Planificacion
{
    [JsonIgnore]
    public string CodMat { get; set; } = null!;
    [JsonIgnore]
    public string CodTurno { get; set; } = null!;

    public string Año { get; set; } = null!;

    public DateTime? FechaExamen { get; set; }

    public virtual Materia CodMatNavigation { get; set; } = null!;

    public virtual Turno CodTurnoNavigation { get; set; } = null!;
}
