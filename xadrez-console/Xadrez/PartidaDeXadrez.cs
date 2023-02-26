﻿using xadrez_console.Tabuleiro;
using xadrez_console.Tabuleiro.Enum;
using xadrez_console.Tabuleiro.Exceptions;

namespace xadrez_console.Xadrez
{
    public class PartidaDeXadrez
    {
        public TabuleiroTab Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;
        public bool Xeque { get; private set; }
        public Peca VulneravelEnPassant { get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new TabuleiroTab(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Xeque = false;
            VulneravelEnPassant = null;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public void ValidarPosicaoDeOrigem(Posicao posicao)
        {
            if(Tab.Peca(posicao) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if(JogadorAtual != Tab.Peca(posicao).Cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!Tab.Peca(posicao).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if(!Tab.Peca(origem).MovimentoPossivel(destino))
                throw new TabuleiroException("Posição de destino invalida.");
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstaEmCheque(JogadorAtual))
            {
                DesfazerMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em cheque.");
            }

            if (EstaEmCheque(Adversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            if (TesteXequeMate(Adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }
        }

        private void DesfazerMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca peca = Tab.RetirarPeca(destino);
            peca.DecrementarQteMovimentos();

            if(pecaCapturada != null)
            {
                Tab.ColocarPeca(pecaCapturada, destino);
                Capturadas.Remove(pecaCapturada);
            }

            Tab.ColocarPeca(peca, origem);

            if (peca is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);

                Peca torre = Tab.RetirarPeca(destinoTorre);

                torre.DecrementarQteMovimentos();
                Tab.ColocarPeca(torre, origemTorre);
            }

            if (peca is Rei && destino.Coluna == origem.Coluna -+ 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);

                Peca torre = Tab.RetirarPeca(destinoTorre);

                torre.DecrementarQteMovimentos();
                Tab.ColocarPeca(torre, origemTorre);
            }
        }

        private void MudaJogador()
        {
            switch (JogadorAtual)
            {
                case Cor.Branca:
                    JogadorAtual = Cor.Preta; 
                    break;
                default:
                    JogadorAtual = Cor.Branca;
                    break;
            }
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new();

            foreach (var Peca in Capturadas)
            {
                if(Peca.Cor == cor)
                {
                    aux.Add(Peca);
                }
            }

            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new();

            foreach (var Peca in Pecas)
            {
                if (Peca.Cor == cor)
                {
                    aux.Add(Peca);
                }
            }

            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        private Cor Adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
                return Cor.Preta;
            else
                return Cor.Branca;
        }

        private Peca Rei(Cor cor)
        {
            foreach (var peca in PecasEmJogo(cor))
            {
                if(peca is Rei)
                {
                    return peca;
                }
            }

            return null;
        }

        public bool EstaEmCheque(Cor cor)
        {
            Peca rei = Rei(cor);

            if (rei == null)
                throw new TabuleiroException($"Não tem rei da cor {cor} no tabuleiro");

            foreach (var peca in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] mat = peca.MovimentosPossíveis();
                if (mat[rei.Posicao.Linha, rei.Posicao.Coluna])
                {
                    return true;
                }
            }

            return false;
        }

        public bool TesteXequeMate(Cor cor)
        {
            if (!EstaEmCheque(cor))
            {
                return false;
            }

            foreach (var peca in PecasEmJogo(cor))
            {
                bool[,] mat = peca.MovimentosPossíveis();

                for (int linha = 0; linha < Tab.Linhas; linha++)
                {
                    for (int coluna = 0; coluna < Tab.Colunas; coluna++)
                    {
                        if (mat[linha, coluna])
                        {
                            Posicao origem = peca.Posicao;
                            Posicao destino = new(linha, coluna);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = EstaEmCheque(cor);
                            DesfazerMovimento(origem, destino, pecaCapturada);
                            if(!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('a', 1, new Torre(Cor.Branca, Tab));
            ColocarNovaPeca('b', 1, new Cavalo(Cor.Branca, Tab));
            ColocarNovaPeca('c', 1, new Bispo(Cor.Branca, Tab));
            ColocarNovaPeca('d', 1, new Dama(Cor.Branca, Tab));
            ColocarNovaPeca('e', 1, new Rei(Cor.Branca, Tab, this));
            ColocarNovaPeca('f', 1, new Bispo(Cor.Branca, Tab));
            ColocarNovaPeca('g', 1, new Cavalo(Cor.Branca, Tab));
            ColocarNovaPeca('h', 1, new Torre(Cor.Branca, Tab));
            ColocarNovaPeca('a', 2, new Peao(Cor.Branca, Tab, this));
            ColocarNovaPeca('b', 2, new Peao(Cor.Branca, Tab, this));
            ColocarNovaPeca('c', 2, new Peao(Cor.Branca, Tab, this));
            ColocarNovaPeca('d', 2, new Peao(Cor.Branca, Tab, this));
            ColocarNovaPeca('e', 2, new Peao(Cor.Branca, Tab, this));
            ColocarNovaPeca('f', 2, new Peao(Cor.Branca, Tab, this));
            ColocarNovaPeca('g', 2, new Peao(Cor.Branca, Tab, this));
            ColocarNovaPeca('h', 2, new Peao(Cor.Branca, Tab, this));

            ColocarNovaPeca('a', 8, new Torre(Cor.Preta, Tab));
            ColocarNovaPeca('b', 8, new Cavalo(Cor.Preta, Tab));
            ColocarNovaPeca('c', 8, new Bispo(Cor.Preta, Tab));
            ColocarNovaPeca('d', 8, new Dama(Cor.Preta, Tab));
            ColocarNovaPeca('e', 8, new Rei(Cor.Preta, Tab, this));
            ColocarNovaPeca('f', 8, new Bispo(Cor.Preta, Tab));
            ColocarNovaPeca('g', 8, new Cavalo(Cor.Preta, Tab));
            ColocarNovaPeca('h', 8, new Torre(Cor.Preta, Tab));
            ColocarNovaPeca('a', 7, new Peao(Cor.Preta, Tab, this));
            ColocarNovaPeca('b', 7, new Peao(Cor.Preta, Tab, this));
            ColocarNovaPeca('c', 7, new Peao(Cor.Preta, Tab, this));
            ColocarNovaPeca('d', 7, new Peao(Cor.Preta, Tab, this));
            ColocarNovaPeca('e', 7, new Peao(Cor.Preta, Tab, this));
            ColocarNovaPeca('f', 7, new Peao(Cor.Preta, Tab, this));
            ColocarNovaPeca('g', 7, new Peao(Cor.Preta, Tab, this));
            ColocarNovaPeca('h', 7, new Peao(Cor.Preta, Tab, this));
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = Tab.RetirarPeca(origem);
            peca.IncrementarQteMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(peca, destino);

            if(pecaCapturada != null)
            {
                Capturadas.Add(pecaCapturada);
            }

            if(peca is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);

                Peca torre = Tab.RetirarPeca(origemTorre);

                torre.IncrementarQteMovimentos();
                Tab.ColocarPeca(torre, destinoTorre);
            }

            if (peca is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);

                Peca torre = Tab.RetirarPeca(origemTorre);

                torre.IncrementarQteMovimentos();
                Tab.ColocarPeca(torre, destinoTorre);
            }

            return pecaCapturada;
        }
    }
}
