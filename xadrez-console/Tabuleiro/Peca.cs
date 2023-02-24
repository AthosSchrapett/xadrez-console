using xadrez_console.Tabuleiro.Enum;

namespace xadrez_console.Tabuleiro
{
    public abstract class Peca
    {
        public Posicao? Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QteMovimentos { get; set; }
        public TabuleiroTab Tab { get; set; }

        public Peca(Cor cor, TabuleiroTab tab)
        {
            Posicao = null;
            Cor = cor;
            Tab = tab;
            QteMovimentos = 0;
        }

        public bool PodeMoverPara(Posicao posicao)
        {
            return MovimentosPossíveis()[posicao.Linha, posicao.Coluna];
        }

        public bool ExisteMovimentosPossiveis()
        {
            bool[,] mat = MovimentosPossíveis();

            for (int linha = 0; linha < Tab.Linhas; linha++)
            {
                for (int coluna = 0; coluna < Tab.Colunas; coluna++)
                {
                    if (mat[linha, coluna])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public abstract bool[,] MovimentosPossíveis();

        public void IncrementarQteMovimentos()
        {
            QteMovimentos++;
        }
    }
}
