using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API_TrabajoFinal.Models;

public partial class Titulo
{
    public string CodTitulos { get; set; } = null!;

    public string? DescTitulo { get; set; }
    [JsonIgnore]
    public virtual ICollection<Profesor> Profesores { get; set; } = new List<Profesor>();
}
