using System;
using System.Diagnostics;
using System.Security.Policy;
using tabuleiro;

namespace xadrez
{
    class Peao : Peca
    {

        public Peao(Tabuleiro tab, Cor cor) : base(tab, cor) { }


        private bool livre(Posicao pos)
        {
            return tab.peca(pos) == null;
        }

        private bool existeInimigo(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p != null && p.cor != cor;
        }




        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];
            Posicao pos = new Posicao(0, 0);

            if (cor == Cor.Branca) 
            {
                pos.definirValores(posicao.linha - 1, posicao.coluna);
                if (tab.PosicaoValida(pos) && livre(pos)){
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha - 2, posicao.coluna);
                if (tab.PosicaoValida(pos) && livre(pos) && qteMovimento == 0)
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
                if (tab.PosicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
                if (tab.PosicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
            }
            else
            {
                pos.definirValores(posicao.linha + 1, posicao.coluna);
                if (tab.PosicaoValida(pos) && livre(pos)){
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha + 2, posicao.coluna);
                if (tab.PosicaoValida(pos) && livre(pos) && qteMovimento == 0)
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
                if (tab.PosicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
                if (tab.PosicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }

            }


          

            return mat;
        }





        public override string ToString()
        {
            return "P";
        }


    }


}