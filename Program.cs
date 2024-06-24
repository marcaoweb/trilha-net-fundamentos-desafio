using Spectre.Console;
using DesafioFundamentos;
using DesafioFundamentos.EntityModels;
using DesafioFundamentos.Menu;
using DesafioFundamentos.Repositories;
using DesafioFundamentos.Utils;

Console.OutputEncoding = System.Text.Encoding.UTF8;

EstacionamentoContext context = new();
IVeiculoRepository veiculo = new VeiculoRepository(context);
IReciboRepository recibo = new ReciboRepository(context);

Estacionamento es = new(veiculo, recibo);

bool exibirMenu = true;

// Loop menu principal
do
{
    var titulo = new Rule("[white][bold]Gerenciador Estacionamento V2.0[/][/]")
        .Centered()
        .HeavyBorder();
    AnsiConsole.Write(titulo);

    switch (Menu.MenuPrincipal())
    {
        case "Entrada":
            Entrada();
            Clear();
            break;
        case "Saida":
            Saida();
            Clear();
            break;
        case "Listar":
            Listar();
            Clear();
            break;
        case "Fechar":
            Clear();
            exibirMenu = false;
            break;
        default:
            exibirMenu = true;
            break;
    }

} while(exibirMenu);

var messagemFechar = new Rule("[red][bold]O programa se encerrou...[/][/]")
    .Centered().HeavyBorder().RuleStyle("red");
AnsiConsole.Write(messagemFechar);

async void Entrada()
{
    Helpers.Titulo("Estacionar Veículo:", "blue", "blue");

    var placa = Helpers.PromptPlaca();

    // chamo a função cadastro veiculo para que insira o veiculo caso não exista
    Veiculo? veiculo = await es.CadastroVeiculo(placa);

    // verifico se esta estacionado
    Recibo? estacionado = await es.VeiculoEstacionado(veiculo.VeiculoId);

    // caso o veiculo esteja estacionado
    if(estacionado is not null)
    {
        Helpers.Mensagem("lime", "Veiculo já estacionado!");
        Helpers.VoltarMenu("white", "bold");
        return;
    }

    // estaciono o veiculo
    Recibo? recibo = await es.EstacionaVeiculo(veiculo.VeiculoId);
      
}

async void Saida()
{
    Helpers.Titulo("Retirar Veículo:", "blue", "blue");

    var placa = Helpers.PromptPlaca();

    // verifico se o veiculo esta cadastrado
    Veiculo? veiculo = await es.CadastroVeiculo(placa);

    // verifico se esta estacionado
    Recibo? recibo = await es.VeiculoEstacionado(veiculo.VeiculoId);

    // caso não esteja estacionado
    if(recibo is null)
    {
        Helpers.Mensagem("orangered1", "Veículo não esta estacionado!");
        Helpers.VoltarMenu("white", "bold");
        return;
    }

    // retiro o veiculo e exibo o recibo
    Recibo? reciboFinal = await es.RetiraVeiculoEstacionamento(recibo);

}

async void Listar()
{
    Helpers.Titulo("Veículos Estacionados:", "blue", "blue");

    var estacionados = await es.ListaVeiculosEstacionados();
    
}