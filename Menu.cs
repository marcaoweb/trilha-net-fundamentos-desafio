using Spectre.Console;

namespace DesafioFundamentos.Menu;

public class Menu
{
    public static string MenuPrincipal()
    {
        var itensMenu = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .MoreChoicesText("[grey]Use seta para cima/baixo para ver outras opções")
                .HighlightStyle("gold1")
                .AddChoices(new[] {"Entrada", "Saida", "Listar", "Fechar"
            })
        );
        return itensMenu;
    }
}