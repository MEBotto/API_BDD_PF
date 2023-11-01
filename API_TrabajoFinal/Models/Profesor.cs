using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API_TrabajoFinal.Models;

public partial class Profesor
{
    public int NroLegajoP { get; set; }

    public string? ApeNomb { get; set; }

    public int? NroDoc { get; set; }
    [JsonIgnore]
    public string? CodDoc { get; set; }
    [JsonIgnore]
    public string? Direccion { get; set; }

    public string? Email { get; set; }
    [JsonIgnore]
    public string? Telefono { get; set; }

    public string? Sexo { get; set; }

    public DateTime? FecNac { get; set; }
    [JsonIgnore]
    public string? EstCivil { get; set; }
    [JsonIgnore]
    public string? CodTitulo { get; set; }
    [JsonIgnore]
    public virtual TipoDoc? CodDocNavigation { get; set; }

    public virtual Titulo? CodTituloNavigation { get; set; }
    [JsonIgnore]
    public virtual ICollection<Materia> Materia { get; set; } = new List<Materia>();
}
