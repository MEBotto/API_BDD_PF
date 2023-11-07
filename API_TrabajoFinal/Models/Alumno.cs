using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API_TrabajoFinal.Models;

public partial class Alumno
{
    public int NroLegajoA { get; set; }

    public string? ApeNomb { get; set; }

    public int? NroDoc { get; set; }

    public string? Direccion { get; set; }

    public string? Email { get; set; }

    public string? Telefono { get; set; }

    public string? CodDoc { get; set; }

    public string? Sexo { get; set; }

    public DateTime? FecNac { get; set; }

    public string? EstCivil { get; set; }

    public virtual TipoDoc? CodDocNavigation { get; set; }
    [JsonIgnore]
    public virtual ICollection<Examen> Examenes { get; set; } = new List<Examen>();
}
