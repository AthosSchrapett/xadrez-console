using xadrez_console.Tabuleiro;
using xadrez_console.Tabuleiro.Enum;
using xadrez_console.Xadrez;

namespace xadrez_console
{
    public class Tela
    {
        public static void ImprimirPartida(PartidaDeXadrez partidaDeXadrez)
        {
            ImprimirTabuleiro(partidaDeXadrez.Tab);
            Console.WriteLine();
            ImprimirPecasCapturadas(partidaDeXadrez);
            Console.WriteLine();
            Console.WriteLine($"Turno: {partidaDeXadrez.Turno}");

            if (!partidaDeXadrez.Terminada)
            {
                Console.WriteLine($"Aguardando Jogada: {partidaDeXadrez.JogadorAtual}");

                if (partidaDeXadrez.Xeque)
                {
                    Console.WriteLine("XEQUE!");
                }
            }
            else
            {
                Console.WriteLine("XEQUEMATE!");
                Console.WriteLine($"Vencedor: {partidaDeXadrez.JogadorAtual}");
            }
        }

        public static void ImprimirTabuleiro(TabuleiroTab tab)
        {
            for (int linha = 0; linha < tab.Linhas; linha++)
            {
                Console.Write($"{8 - linha} ");

                for (int coluna = 0; coluna < tab.Colunas; coluna++)
                {
                    ImprimirPeca(tab.Peca(linha, coluna));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirTabuleiro(TabuleiroTab tab, bool[,] posicoesPossiveis)
        {

            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int linha = 0; linha < tab.Linhas; linha++)
            {
                Console.Write($"{8 - linha} ");

                for (int coluna = 0; coluna < tab.Colunas; coluna++)
                {
                    if (posicoesPossiveis[linha, coluna])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    ImprimirPeca(tab.Peca(linha, coluna));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = fundoOriginal;
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];

            int linha = int.Parse(s[1].ToString());

            return new PosicaoXadrez(coluna, linha);
        }

        public static void ImprimirPeca(Peca peca)
        {
            if(peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.Cor == Cor.Branca)
                    Console.Write(peca);
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }

                Console.Write(" ");
            }
        }

        private static void ImprimirPecasCapturadas(PartidaDeXadrez partidaDeXadrez)
        {
            Console.WriteLine("Peças capturadas:");
            Console.Write("Brancas: ");
            ImprimirConjunto(partidaDeXadrez.PecasCapturadas(Cor.Branca));
            Console.WriteLine();
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            ImprimirConjunto(partidaDeXadrez.PecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        private static void ImprimirConjunto(HashSet<Peca> pecas)
        {
            Console.Write("[");
            foreach (Peca peca in pecas)
            {
                Console.Write(peca + " ");
            }
            Console.Write("]");
        }
    }
}
