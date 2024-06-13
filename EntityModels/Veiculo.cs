using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DesafioFundamentos.EntityModels;

public partial class Veiculo
{
    [Key]
    public int VeiculoId { get; set; }

    [Column(TypeName = "nvarchar (7)")]
    public string Placa { get; set; } = null!;

    [InverseProperty("Veiculo")]
    public virtual ICollection<Recibo> Recibos { get; set; } = new List<Recibo>();
}
