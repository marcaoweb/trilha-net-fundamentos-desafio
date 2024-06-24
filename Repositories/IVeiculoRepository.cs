using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioFundamentos.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace DesafioFundamentos.Repositories;

public interface IVeiculoRepository
{
    Task<Veiculo?> InserirVeiculo(string placa);
    Task<Veiculo?> ObterVeiculo(string placa);
    Task<List<Veiculo>> ObterVeiculos();

}