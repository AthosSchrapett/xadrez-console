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
        try
        {
            Console.Clear();
            Tela.ImprimirTabuleiro(partidaDeXadrez.Tab);
            Console.WriteLine();
            Console.WriteLine($"Turno: {partidaDeXadrez.Turno}");
            Console.WriteLine($"Aguardando Jogada: {partidaDeXadrez.JogadorAtual}");

            Console.Write("Origem: ");
            Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
            partidaDeXadrez.ValidarPosicaoDeOrigem(origem);

            bool[,] posicoesPossiveis = partidaDeXadrez.Tab.Peca(origem).MovimentosPossíveis();

            Console.Clear();
            Tela.ImprimirTabuleiro(partidaDeXadrez.Tab, posicoesPossiveis);

            Console.Write("Destino: ");
            Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
            partidaDeXadrez.ValidarPosicaoDeDestino(origem, destino);

            partidaDeXadrez.RealizaJogada(origem, destino);
        }
        catch (TabuleiroException e)
        {
            Console.WriteLine(e.Message);
            Console.ReadLine();
        }
    }

    Tela.ImprimirTabuleiro(partidaDeXadrez.Tab);
}
catch (TabuleiroException e)
{
    Console.WriteLine(e.Message);
}

Console.ReadLine();