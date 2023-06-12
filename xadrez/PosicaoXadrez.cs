using System;
using tabuleiro;
namespace xadrez
{

    class PosicaoXadrez
    {
        public char coluna { get; set; }
        public int linha { get; set; }

        public PosicaoXadrez(char coluna, int linha) 
        {
            this.coluna = coluna;
            this.linha = linha;
        }

        public PosicaoXadrez toPosicao()
        {
            return new PosicaoXadrez(8 - linha, coluna - 'a');
        }


        public override string ToString()
        {
            return "" + coluna + linha;
        }
    }
}
