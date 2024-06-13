using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DesafioFundamentos.EntityModels;

public partial class Recibo
{
    [Key]
    public int ReciboId { get; set; }

    [Column(TypeName = "INT")]
    public int? VeiculoId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime HoraEntrada { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? HoraSaida { get; set; }

    [Column(TypeName = "money")]
    public decimal? PrecoFixo { get; set; }

    [Column(TypeName = "money")]
    public decimal? PrecoPorHora { get; set; }

    [Column(TypeName = "bit")]
    public bool Status { get; set; }

    [Column(TypeName = "money")]
    public decimal? Total { get; set; }

    [ForeignKey("VeiculoId")]
    [InverseProperty("Recibos")]
    public virtual Veiculo? Veiculo { get; set; }
}
