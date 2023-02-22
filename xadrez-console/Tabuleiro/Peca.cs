using xadrez_console.Tabuleiro.Enum;

namespace xadrez_console.Tabuleiro
{
    public class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QteMovimentos { get; set; }
        public TabuleiroTab Tab { get; set; }

        public Peca(Posicao posicao, Cor cor, TabuleiroTab tab)
        {
            Posicao = posicao;
            Cor = cor;
            Tab = tab;
            QteMovimentos = 0;
        }
    }
}
