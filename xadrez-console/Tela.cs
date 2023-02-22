using xadrez_console.Tabuleiro;
using xadrez_console.Tabuleiro.Enum;

namespace xadrez_console
{
    public class Tela
    {
        public static void ImprimirTabuleiro(TabuleiroTab tab)
        {
            for (int linha = 0; linha < tab.Linhas; linha++)
            {
                Console.Write($"{8 - linha} ");

                for (int coluna = 0; coluna < tab.Colunas; coluna++)
                {
                    if(tab.Peca(linha, coluna) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        ImprimirPeca(tab.Peca(linha, coluna));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirPeca(Peca peca)
        {
            if(peca.Cor == Cor.Branca)
                Console.Write(peca);
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
        }
    }
}
