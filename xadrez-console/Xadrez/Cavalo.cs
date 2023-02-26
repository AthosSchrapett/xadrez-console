using xadrez_console.Tabuleiro;
using xadrez_console.Tabuleiro.Enum;

namespace xadrez_console.Xadrez
{
    public class Cavalo : Peca
    {
        public Cavalo(Cor cor, TabuleiroTab Tab) : base(cor, Tab)
        {
        }

        public override bool[,] MovimentosPossíveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao posicao = new Posicao(0,0);

            posicao.DefinirValores(posicao.Linha - 1, posicao.Coluna - 2);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.DefinirValores(posicao.Linha - 2, posicao.Coluna - 1);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.DefinirValores(posicao.Linha - 2, posicao.Coluna + 1);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.DefinirValores(posicao.Linha - 1, posicao.Coluna + 2);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.DefinirValores(posicao.Linha + 1, posicao.Coluna + 2);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.DefinirValores(posicao.Linha + 2, posicao.Coluna + 1);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.DefinirValores(posicao.Linha + 2, posicao.Coluna - 1);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            posicao.DefinirValores(posicao.Linha + 1, posicao.Coluna - 2);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            return mat;
        }

        private bool PodeMover(Posicao posicao)
        {
            Peca peca = Tab.Peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        public override string ToString()
        {
            return "C";
        }
    }
}
