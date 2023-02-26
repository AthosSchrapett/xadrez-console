using System.Runtime.ConstrainedExecution;
using xadrez_console.Tabuleiro;
using xadrez_console.Tabuleiro.Enum;

namespace xadrez_console.Xadrez
{
    public class Peao : Peca
    {
        private PartidaDeXadrez Partida;

        public Peao(Cor cor, TabuleiroTab Tab, PartidaDeXadrez partida) : base(cor, Tab)
        {
            Partida = partida;
        }

        public override bool[,] MovimentosPossíveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao posicao = new Posicao(0, 0);

            if (Cor == Cor.Branca)
            {
                posicao.DefinirValores(posicao.Linha - 1, posicao.Coluna);
                if (Tab.PosicaoValida(posicao) && Livre(posicao))
                {
                    mat[posicao.Linha, posicao.Coluna] = true;
                }
                posicao.DefinirValores(posicao.Linha - 2, posicao.Coluna);
                Posicao posicao2 = new Posicao(posicao.Linha - 1, posicao.Coluna);
                if (Tab.PosicaoValida(posicao2) && Livre(posicao2) && Tab.PosicaoValida(posicao) && Livre(posicao) && QteMovimentos == 0)
                {
                    mat[posicao.Linha, posicao.Coluna] = true;
                }
                posicao.DefinirValores(posicao.Linha - 1, posicao.Coluna - 1);
                if (Tab.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    mat[posicao.Linha, posicao.Coluna] = true;
                }
                posicao.DefinirValores(posicao.Linha - 1, posicao.Coluna + 1);
                if (Tab.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    mat[posicao.Linha, posicao.Coluna] = true;
                }

                // #jogadaespecial en passant
                if (posicao.Linha == 3)
                {
                    Posicao esquerda = new Posicao(posicao.Linha, posicao.Coluna - 1);
                    if (Tab.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && Tab.Peca(esquerda) == Partida.VulneravelEnPassant)
                    {
                        mat[esquerda.Linha - 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(posicao.Linha, posicao.Coluna + 1);
                    if (Tab.PosicaoValida(direita) && ExisteInimigo(direita) && Tab.Peca(direita) == Partida.VulneravelEnPassant)
                    {
                        mat[direita.Linha - 1, direita.Coluna] = true;
                    }
                }
            }
            else
            {
                posicao.DefinirValores(posicao.Linha + 1, posicao.Coluna);
                if (Tab.PosicaoValida(posicao) && Livre(posicao))
                {
                    mat[posicao.Linha, posicao.Coluna] = true;
                }
                posicao.DefinirValores(posicao.Linha + 2, posicao.Coluna);
                Posicao posicao2 = new Posicao(posicao.Linha + 1, posicao.Coluna);
                if (Tab.PosicaoValida(posicao2) && Livre(posicao2) && Tab.PosicaoValida(posicao) && Livre(posicao) && QteMovimentos == 0)
                {
                    mat[posicao.Linha, posicao.Coluna] = true;
                }
                posicao.DefinirValores(posicao.Linha + 1, posicao.Coluna - 1);
                if (Tab.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    mat[posicao.Linha, posicao.Coluna] = true;
                }
                posicao.DefinirValores(posicao.Linha + 1, posicao.Coluna + 1);
                if (Tab.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    mat[posicao.Linha, posicao.Coluna] = true;
                }

                // #jogadaespecial en passant
                if (posicao.Linha == 4)
                {
                    Posicao esquerda = new Posicao(posicao.Linha, posicao.Coluna - 1);
                    if (Tab.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && Tab.Peca(esquerda) == Partida.VulneravelEnPassant)
                    {
                        mat[esquerda.Linha + 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(posicao.Linha, posicao.Coluna + 1);
                    if (Tab.PosicaoValida(direita) && ExisteInimigo(direita) && Tab.Peca(direita) == Partida.VulneravelEnPassant)
                    {
                        mat[direita.Linha + 1, direita.Coluna] = true;
                    }
                }
            }

            return mat;
        }

        private bool ExisteInimigo(Posicao pos)
        {
            Peca peca = Tab.Peca(pos);
            return peca != null && peca.Cor != Cor;
        }

        private bool Livre(Posicao posicao)
        {
            return Tab.Peca(posicao) == null;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
