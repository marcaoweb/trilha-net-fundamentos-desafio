using Spectre.Console;
using Microsoft.EntityFrameworkCore;
using DesafioFundamentos.Repositories;
using DesafioFundamentos.Utils;
using DesafioFundamentos.EntityModels;

namespace DesafioFundamentos;

public class Estacionamento
{

    private readonly IVeiculoRepository _veiculoRepo;
    private readonly IReciboRepository _reciboRepo;


    public Estacionamento(IVeiculoRepository vRepo, IReciboRepository rRepo)
    {
        _veiculoRepo = vRepo;
        _reciboRepo = rRepo;
    }

    public async Task<Veiculo?> CadastroVeiculo(string placa)
    {
        Veiculo? veiculo = await _veiculoRepo.InserirVeiculo(placa);
        
        if(veiculo is null)
        {
            Helpers.MensagemErro();
            Helpers.VoltarMenu("white", "bold");
            return null;
        }
        return veiculo;
    }

    public async Task<Recibo?> VeiculoEstacionado(int veiculoId)
    {
        Recibo? reciboEstacionado = await _reciboRepo.ObterReciboEstacionado(veiculoId);
        if(reciboEstacionado is null) 
        { 
            return null; 
        }
        Helpers.Recibo(reciboEstacionado);
        return reciboEstacionado;
    }

    public async Task<Recibo?> RetiraVeiculoEstacionamento(Recibo recibo)
    {
        var promptRetirar = AnsiConsole.Prompt(
            new ConfirmationPrompt($"[red1]Deseja remover o veículo {recibo.Veiculo?.Placa}?[/]").No('N').Yes('S')
                .InvalidChoiceMessage("Você deve escolher entre N para Não e S para Sim").ChoicesStyle(Color.Grey42)
        );
        
        
        if(!promptRetirar)
        {
            Helpers.Mensagem("lime", "Veículo permanecerá estacionado.");
            Helpers.VoltarMenu("white", "bold");
            return null;
        }
        Clear();
        DateTime horaSaida = DateTime.Now;
        var tempo = (horaSaida - recibo.HoraEntrada).TotalHours;
        var tempoTotal = tempo < 1.0 ? 1 : (int)tempo;
        recibo.HoraSaida = horaSaida;
        recibo.Total = recibo.PrecoFixo + recibo.PrecoPorHora * tempoTotal;
        recibo.Status = false;

        Recibo? reciboFinal = await _reciboRepo.AtualizarRecibo(recibo);
        if(reciboFinal is null) 
        {
            Helpers.MensagemErro(""); 
            Helpers.VoltarMenu("white", "bold"); 
            return null; 
        }

        Helpers.Recibo(reciboFinal);
        Helpers.Mensagem("green", "Veículo retirado com sucesso!");
        Helpers.VoltarMenu("white", "bold");
        return reciboFinal;
    }

    public async Task<Recibo?> EstacionaVeiculo(int veiculoId)
    {
        var precoFixo = Helpers.PromptPreco("Preço Fixo:");
        var precoPorHora = Helpers.PromptPreco("Preço Por Hora:");

        Recibo recibo = await _reciboRepo.InserirRecibo(veiculoId, precoFixo, precoPorHora);
        if(recibo is null) 
        { 
            Helpers.MensagemErro("Não foi possivel estacionar o veiculo, por favor tente novamente.");
            Helpers.VoltarMenu("white", "bold");
            return null;
        }

        Helpers.Recibo(recibo);
        Helpers.Mensagem("green", $"O veiculo {recibo.Veiculo?.Placa} foi estacionado com sucesso!");
        Helpers.VoltarMenu("white", "bold");
        return recibo;
    }

    public async Task<List<Recibo>> ListaVeiculosEstacionados()
    {
        List<Recibo> listaVeiculos = new List<Recibo>();
        listaVeiculos = await _reciboRepo.ObterRecibosEstacionados();
        var mensagem = listaVeiculos.Count == 0 ? "Estacionamento vazio!" : "";
        
        Helpers.Mensagem("red", $"{mensagem}");
        if(listaVeiculos.Count > 0)
        {
            Helpers.TabelaVeiculos(listaVeiculos);
        }        
        Helpers.VoltarMenu("white", "bold");
        return listaVeiculos;
    }

}