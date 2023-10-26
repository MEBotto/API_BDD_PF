using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API_TrabajoFinal.Models;

public partial class TipoDoc
{
    public string CodDoc { get; set; } = null!;

    public string? DescDoc { get; set; }

    [JsonIgnore]
    public virtual ICollection<Alumno> Alumnos { get; set; } = new List<Alumno>();
    [JsonIgnore]
    public virtual ICollection<Profesor> Profesores { get; set; } = new List<Profesor>();
}
