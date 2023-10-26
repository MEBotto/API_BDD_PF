using System;
using System.Collections.Generic;

namespace API_TrabajoFinal.Models;

public partial class Profesor
{
    public int NroLegajoP { get; set; }

    public string? ApeNomb { get; set; }

    public int? NroDoc { get; set; }

    public string? CodDoc { get; set; }

    public string? Direccion { get; set; }

    public string? Email { get; set; }

    public string? Telefono { get; set; }

    public string? Sexo { get; set; }

    public DateTime? FecNac { get; set; }

    public string? EstCivil { get; set; }

    public string? CodTitulo { get; set; }

    public virtual TipoDoc? CodDocNavigation { get; set; }

    public virtual Titulo? CodTituloNavigation { get; set; }

    public virtual ICollection<Materia> Materia { get; set; } = new List<Materia>();
}
