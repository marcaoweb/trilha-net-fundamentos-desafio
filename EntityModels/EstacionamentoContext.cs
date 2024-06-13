using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DesafioFundamentos.EntityModels;

public partial class EstacionamentoContext : DbContext
{
    public EstacionamentoContext()
    {
    }

    public EstacionamentoContext(DbContextOptions<EstacionamentoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Recibo> Recibos { get; set; }

    public virtual DbSet<Veiculo> Veiculos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=Estacionamento.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Recibo>(entity =>
        {
            entity.Property(e => e.PrecoFixo).HasDefaultValue("1");
            entity.Property(e => e.PrecoPorHora).HasDefaultValue("1");

            entity.Property(e => e.PrecoFixo).HasConversion<double>();
            entity.Property(e => e.PrecoPorHora).HasConversion<double>();
            entity.Property(e => e.Total).HasConversion<double>();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
