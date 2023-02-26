using xadrez_console.Tabuleiro;
using xadrez_console.Tabuleiro.Enum;

namespace xadrez_console.Xadrez
{
    public class Rei : Peca
    {
        private PartidaDeXadrez Partida;

        public Rei(Cor cor, TabuleiroTab tab, PartidaDeXadrez partida) : base(cor, tab)
        {
            Partida = partida;
        }

        private bool PodeMover(Posicao posicao)
        {
            Peca peca = Tab.Peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        private bool TesteTorreParaRoque(Posicao posicao)
        {
            Peca peca = Tab.Peca(Posicao);

            return peca != null && peca is Torre && peca.Cor == Cor && peca.QteMovimentos == 0;
        }

        public override bool[,] MovimentosPossíveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao posicao = new(0,0);

            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);

            if(Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);

            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);

            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);

            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);

            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);

            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);

            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);

            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            if(QteMovimentos == 0 && !Partida.Xeque)
            {
                Posicao posicaoTorreRoquePequeno = new(Posicao.Linha, Posicao.Coluna + 3);

                if (TesteTorreParaRoque(posicaoTorreRoquePequeno))
                {
                    Posicao posicao1 = new(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao posicao2 = new(Posicao.Linha, Posicao.Coluna + 2);

                    if(Tab.Peca(posicao1) == null && Tab.Peca(posicao2) == null)
                    {
                        mat[posicao.Linha, posicao.Coluna + 2] = true;
                    }
                }

                Posicao posicaoTorreRoqueGrande = new(Posicao.Linha, Posicao.Coluna - 4);

                if (TesteTorreParaRoque(posicaoTorreRoqueGrande))
                {
                    Posicao posicao1 = new(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao posicao2 = new(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao posicao3 = new(Posicao.Linha, Posicao.Coluna - 3);

                    if (
                        Tab.Peca(posicao1) == null && 
                        Tab.Peca(posicao2) == null && 
                        Tab.Peca(posicao3) == null
                       )
                    {
                        mat[posicao.Linha, posicao.Coluna + 2] = true;
                    }
                }
            }

            return mat;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
