using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioFundamentos.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DesafioFundamentos.Repositories;

public class VeiculoRepository : IVeiculoRepository
{
    private EstacionamentoContext _context;

    public VeiculoRepository(EstacionamentoContext context)
    {
        _context = context;
    }

    
    public async Task<Veiculo?> InserirVeiculo(string placa)
    {
        Veiculo novoVeiculo = new()
        {
            Placa = placa
        };
        // procuro pelo veiculo no banco de dados
        Veiculo? veiculo = await _context.Veiculos.FirstOrDefaultAsync(v => v.Placa == placa);
        
        // caso nao exista, insiro no banco de dados
        if(veiculo is null)
        {
            EntityEntry<Veiculo> veiculoAdd = await _context.Veiculos.AddAsync(novoVeiculo);
            int resultado = await _context.SaveChangesAsync();
            // var resultado = 0;
            if(resultado > 0) 
            { 
                return novoVeiculo;
            }
            return null; 
        }
        return veiculo;
    }

    public async Task<Veiculo?> ObterVeiculo(string placa)
    {
        Veiculo? veiculo = await _context.Veiculos.FirstOrDefaultAsync(v => v.Placa == placa);
        if(veiculo is null) { return null; }
        return veiculo;
    }

    public async Task<List<Veiculo>> ObterVeiculos()
    {
        return await _context.Veiculos.ToListAsync();
    }

}