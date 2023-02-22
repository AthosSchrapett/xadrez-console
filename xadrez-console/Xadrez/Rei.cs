using xadrez_console.Tabuleiro;
using xadrez_console.Tabuleiro.Enum;

namespace xadrez_console.Xadrez
{
    public class Rei : Peca
    {
        public Rei(Cor cor, TabuleiroTab tab) : base(cor, tab)
        {
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
