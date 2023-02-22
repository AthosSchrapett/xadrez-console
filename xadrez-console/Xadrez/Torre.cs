using xadrez_console.Tabuleiro;
using xadrez_console.Tabuleiro.Enum;

namespace xadrez_console.Xadrez
{
    public class Torre : Peca
    {
        public Torre(Cor cor, TabuleiroTab tab) : base(cor, tab)
        {
        }

        public override string ToString()
        {
            return "T";
        }
    }
}