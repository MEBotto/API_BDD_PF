using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API_TrabajoFinal.Models;

public partial class Materia
{
    public string? CodMateria { get; set; } = null!;

    public string? DescMat { get; set; }

    public string? DescCarrera { get; set; }

    public int? NroLegajoP { get; set; }
    [JsonIgnore]
    public virtual ICollection<Examen> Examenes { get; set; } = new List<Examen>();

    public virtual Profesor? NroLegajoPNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Planificacion> Planificacions { get; set; } = new List<Planificacion>();
}
