using xadrez_console.Tabuleiro;

namespace xadrez_console
{
    public class Tela
    {
        public static void ImprimirTabuleiro(TabuleiroTab tab)
        {
            for (int linha = 0; linha < tab.Linhas; linha++)
            {
                for (int coluna = 0; coluna < tab.Colunas; coluna++)
                {
                    if(tab.Peca(linha, coluna) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(tab.Peca(linha, coluna) + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
