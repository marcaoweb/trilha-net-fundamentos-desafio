using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioFundamentos.EntityModels;

namespace DesafioFundamentos.Repositories;

public interface IReciboRepository
{
    Task<Recibo> InserirRecibo(int veiculoId, decimal precoFixo, decimal precoPorHora);
    Task<Recibo?> ObterRecibo(int veiculoId);
    Task<List<Recibo>> ObterRecibos();
    Task<List<Recibo>> ObterRecibosEstacionados();
    Task<Recibo?> ObterReciboEstacionado(int veiculoId);
    Task<Recibo?> AtualizarRecibo(Recibo r);

}