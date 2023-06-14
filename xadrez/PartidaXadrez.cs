using System;
using System.Diagnostics;
using System.Collections.Generic;
using tabuleiro;
using System.Security.Cryptography.X509Certificates;

namespace xadrez
{
    class PartidaXadrez 
    {

        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque { get; private set; }

        public PartidaXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            xeque = false;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

    public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.retirarPeca(destino);
            p.decrementarqteMovimentos();
            if(pecaCapturada != null)
            {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p, origem);
        }



        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarqteMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if(pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
           Peca pecaCapturada =executaMovimento(origem, destino);
            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroExceptions("Voce n�o pode se colocar em Xeque !!!");
            }
            if (estaEmXeque(adversario(jogadorAtual)))
            {
                xeque = true;
            } else {
                xeque = false;
            }

            if (texteXequeMate(adversario(jogadorAtual)))
            {
                terminada = true;
            }
            else
            {

                turno++;
                mudaJogador();
            }
        }

        public void validarPosicaoOrigem(Posicao pos)
        {
            if (tab.peca(pos) == null)
            {
                throw new TabuleiroExceptions("N�o Existe pe�a na posi��o de origem escolhida !!");
            }
            if(jogadorAtual != tab.peca(pos).cor)
            {
                throw new TabuleiroExceptions("A Pe�a de origem escolhida n�o � sua !");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroExceptions("N�o H� movimentos possiveis para pe�a de origem escolhida!");
            }
        }

        public void validarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroExceptions("Posi��o de destino inv�lida !");
            }
        }

        private void mudaJogador()
        {
            if(jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if(x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca> ();
            foreach(Peca x in pecas)
            {
                if(x.cor == cor)
                {
                    aux.Add(x);
                }     
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        private Cor adversario(Cor cor)
        {
            if(cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca rei(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }
             public bool estaEmXeque(Cor cor)
            {
                Peca R = rei(cor);
                if(R == null)
                {
                    throw new TabuleiroExceptions("N�o tem Rei da cor " + cor + " no Tabuleiro! ");
                }
                foreach(Peca x in pecasEmJogo(adversario(cor)))
                {
                    bool[,] mat = x.movimentosPossiveis();
                    if (mat[R.posicao.linha, R.posicao.coluna]){
                        return true;
                    }
                }
                return false;
             }



        public bool texteXequeMate(Cor cor)
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }
            foreach(Peca x in pecasEmJogo(cor))
            {
                bool[,] mat = x.movimentosPossiveis();
                for(int i = 0; i<tab.linhas; i++){
                 for(int j = 0; j<tab.colunas; j++){
                        if (mat[i , j])
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(origem ,destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }

                    }
                }
            }
            return true;
        }



            public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas()
        {
            colocarNovaPeca('c', 1,new Torre(tab, Cor.Branca));
            colocarNovaPeca('h', 7, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));
       

            colocarNovaPeca('a', 8, new Rei(tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Torre(tab, Cor.Preta));
           
        }

    }

}