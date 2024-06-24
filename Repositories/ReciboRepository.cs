using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioFundamentos.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DesafioFundamentos.Repositories;

public class ReciboRepository : IReciboRepository
{

    private EstacionamentoContext _context;

    public ReciboRepository(EstacionamentoContext context)
    {
        _context = context;
    }

    public async Task<Recibo> InserirRecibo(int veiculoId, decimal precoFixo, decimal precoPorHora)
    {
        Recibo recibo = new()
        {
            VeiculoId = veiculoId,
            HoraEntrada = DateTime.Now,
            PrecoFixo = precoFixo,
            PrecoPorHora = precoPorHora,
            Status = true
        };
        EntityEntry<Recibo> entity = await _context.Recibos.AddAsync(recibo);
        int resultado = await _context.SaveChangesAsync();
        // int resultado = 0;
        if(resultado > 0)
        {
            return recibo;
        }
        return null!;
    }

    public async Task<Recibo?> ObterRecibo(int veiculoId)
    {
        Recibo? recibo = await _context.Recibos.FirstOrDefaultAsync(r => r.VeiculoId == veiculoId);
        if(recibo is null) { return null; }
        return recibo;
    }

    public async Task<List<Recibo>> ObterRecibos()
    {
        List<Recibo> listaRecibos = await _context.Recibos.ToListAsync();
        return listaRecibos;
    }

    public async Task<List<Recibo>> ObterRecibosEstacionados()
    {
        return await _context.Recibos.Include(v => v.Veiculo).Where(r => r.Status == true).ToListAsync();
    }

    public async Task<Recibo?> ObterReciboEstacionado(int veiculoId)
    {
        Recibo? recibo = await _context.Recibos.FirstOrDefaultAsync(v => v.VeiculoId == veiculoId && v.Status == true);
        if(recibo is null) { return null; }
        return recibo;
    }
    public async Task<Recibo?> AtualizarRecibo(Recibo r)
    {
        _context.Recibos.Update(r);
        var resultado = await _context.SaveChangesAsync();
        // var resultado = 0;
        if(resultado > 0)
        {
            return r;
        }
        return null;
    }

}