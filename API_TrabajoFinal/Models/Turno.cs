using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API_TrabajoFinal.Models;

public partial class Turno
{
    public string CodTurno { get; set; } = null!;

    public string? DescTurno { get; set; }
    [JsonIgnore]
    public virtual ICollection<Examen> Examenes { get; set; } = new List<Examen>();

    public virtual ICollection<Planificacion> Planificacions { get; set; } = new List<Planificacion>();
}
