using xadrez_console;
using xadrez_console.Tabuleiro;
using xadrez_console.Tabuleiro.Enum;
using xadrez_console.Tabuleiro.Exceptions;
using xadrez_console.Xadrez;

try
{
    PartidaDeXadrez partidaDeXadrez = new PartidaDeXadrez();

    while (!partidaDeXadrez.Terminada)
    {
        Console.Clear();
        Tela.ImprimirTabuleiro(partidaDeXadrez.Tab);

        Console.Write("Origem: ");
        Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();

        Console.Write("Destino: ");
        Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();

        partidaDeXadrez.ExecutaMovimento(origem, destino);
    }

    Tela.ImprimirTabuleiro(partidaDeXadrez.Tab);
}
catch (TabuleiroException e)
{
    Console.WriteLine(e.Message);
}

Console.ReadLine();