using DesafioFundamentos.EntityModels;
using Spectre.Console;

namespace DesafioFundamentos.Utils;

public class Helpers
{
    public static void Titulo(string titulo, string corTitulo, string corLinha)
    {
        var rule = new Rule($"[{corTitulo}]{titulo}[/]")
            .LeftJustified().RoundedBorder().RuleStyle($"{corLinha}");
        AnsiConsole.Write(rule);
    }

    public static void TabelaVeiculos(List<Recibo> recibo)
    {
        DateTime data = new DateTime(
            year: DateTime.Now.Year, month: DateTime.Now.Month, day: DateTime.Now.Day,
            hour: DateTime.Now.Hour, minute: DateTime.Now.Minute, second: DateTime.Now.Second
        ); 

        var table = new Table();
        table.Border(TableBorder.MinimalHeavyHead).BorderColor(Color.Blue);
        table.AddColumn(new TableColumn("[yellow]Veiculos[/]").LeftAligned());
        table.AddColumn(new TableColumn("[yellow]Hora Entrada[/]").LeftAligned());
        table.AddColumn(new TableColumn("[yellow]Tempo Total[/]").Centered());
        table.AddColumn(new TableColumn("[yellow]Preço Fixo[/]").Centered());
        table.AddColumn(new TableColumn("[yellow]Preço Por Hora[/]").Centered());
        table.AddColumn(new TableColumn("[yellow]Total Parcial[/]").Centered());        
        
        foreach (var v in recibo)
        {
            var tempo = (data - v.HoraEntrada).TotalHours;       
            var tempoTotal = tempo < 1.0 ? 1 : (int)tempo; 
            var precoTotal = v.PrecoFixo + v.PrecoPorHora * (int)tempoTotal;
            table.AddRow($"{v.Veiculo?.Placa}", $"{v.HoraEntrada}", $"{tempoTotal} hora(s)", 
            $"{v.PrecoFixo:C}", $"{v.PrecoPorHora:C}", $"{precoTotal:C}").ShowRowSeparators();
        };

        AnsiConsole.Write(table);
    }

    public static void Recibo(Recibo recibo)
    {
        var rule = new Rule("[white]| RECIBO |[/]")
            .Centered().SquareBorder().RuleStyle(Color.OrangeRed1);
        AnsiConsole.Write(rule);
        DateTime data = new DateTime(
            year: DateTime.Now.Year, month: DateTime.Now.Month, day: DateTime.Now.Day,
            hour: DateTime.Now.Hour, minute: DateTime.Now.Minute, second: DateTime.Now.Second 
        );
        DateTime horaEntrada = recibo.HoraEntrada;
        DateTime horaSaida = recibo.HoraSaida ?? data;

        var status = (recibo.HoraSaida is null) ? "[green][bold]Aberto[/][/]" : "[red][bold]Fechado[/][/]";
        var tempo = (horaSaida - horaEntrada).TotalHours;       
        var tempoTotal = tempo < 1.0 ? 1 : (int)tempo!;
        var precoTotal = recibo.PrecoFixo + recibo.PrecoPorHora * (int)tempoTotal;

        var table = new Table();

        table.Border(TableBorder.Rounded).BorderColor(Color.White).Centered();
        table.AddColumn(new TableColumn($"[yellow]Recibo id: {recibo.ReciboId}[/]")
                        .Footer($"[yellow]Situação:[/] {status}").LeftAligned());
        table.AddColumn(new TableColumn("[yellow]Horários[/]")
                        .Footer($"[yellow]Tempo Total:[/] {(int)tempoTotal} Hora(s)").LeftAligned());
        table.AddColumn(new TableColumn("[yellow]Valores[/]")
                        .Footer($"[yellow]Total:[/] {precoTotal:C}").RightAligned());

        table.AddRow($"[yellow]Placa:[/] {recibo.Veiculo?.Placa}", $"[yellow]Entrada:[/] {recibo.HoraEntrada}",
                    $"[yellow]Preço Fixo:[/] {recibo.PrecoFixo:C}");
        table.AddRow("", $"[yellow]Saida:[/] {recibo.HoraSaida}", $"[yellow]Preço por Hora:[/] {recibo.PrecoPorHora:C}");

        AnsiConsole.Write(table);
    }

    public static void VoltarMenu(string corTexto, string? formatoTexto)
    {
        AnsiConsole.Write(new Markup($"[{corTexto}][{formatoTexto}]Pressione uma tecla para voltar ao menu principal[/][/]"));
        WriteLine();
        ReadLine();
    }

    public static void MensagemErro(string? mensagem = "Ocorreu um erro inesperado, por favor tente novamente.")
    {
        AnsiConsole.Write(new Markup($"[orangered1][bold]{mensagem}[/][/]"));
        WriteLine();
    }

    public static void Mensagem(string cor, string? mensagem)
    {
        AnsiConsole.Write(new Markup($"[{cor}][bold]{mensagem}[/][/]"));
        WriteLine();
    }

    public static string PromptPlaca()
    {
        var placa = AnsiConsole.Prompt(
            new TextPrompt<string>("[yellow]Digite a placa:[/]")
                .PromptStyle("white italic")
                .ValidationErrorMessage("Esta placa não é válida.")
                .Validate(p =>
                    {
                        return p.Count() switch
                        {
                            < 7 => ValidationResult.Error("[red]Erro: A placa tem menos 7 caracteres. Tente novamente.[/]"),
                            > 7 => ValidationResult.Error("[red]Erro: A placa tem mais 7 caracteres. Tente novamente.[/]"),
                             _ => ValidationResult.Success()
                        };
                    }
        )).ToUpper();
        return placa;
    }

    public static decimal PromptPreco(string messagem)
    {
        var preco = AnsiConsole.Prompt(
            new TextPrompt<decimal>($"[yellow]{messagem}[/]")
            .PromptStyle("blue bold")
            .ValidationErrorMessage("Preco em formato inválido")
            .Validate(p => 
            {
                return p switch
                {
                    < 1 => ValidationResult.Error("[red]Erro: O preço deve ser maior que 0[/]"),
                    _ => ValidationResult.Success()
                };
            })
            .DefaultValue(1).ShowDefaultValue(false)
        );
        return preco;
    }

}