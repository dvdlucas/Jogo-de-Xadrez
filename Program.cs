using System;
using tabuleiro;
using xadrez;


    class Program {
    static void Main(string[] args) {

        try
        {
            PartidaXadrez partida = new PartidaXadrez();
         


           
            while (!partida.terminada)
            {
                Console.WriteLine();
                Console.Clear();
                Tela.imprimirTabuleiro(partida.tab);
                Console.Write("Digite a origem da peça : ");
                Posicao origem = Tela.lerPosicaoXadrez().toPosicao();

                bool[,] posicoesPossiveis = partida.tab.peca(origem).movimentosPossiveis();

                Console.Clear();
                Tela.imprimirTabuleiro(partida.tab , posicoesPossiveis);

                Console.WriteLine();
                Console.Write("Digite o destino da peça : ");
                Posicao destino = Tela.lerPosicaoXadrez().toPosicao();

                partida.executaMovimento(origem, destino);

            }


        }
        catch (TabuleiroExceptions e)
        {
            Console.WriteLine(e.Message);
        }




    }
}