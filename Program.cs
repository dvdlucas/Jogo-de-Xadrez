﻿using System;
using tabuleiro;
using xadrez;


    class Program {
    static void Main(string[] args) {

        try
        {
            PartidaXadrez partida = new PartidaXadrez();
         


           
            while (!partida.terminada)
            {
                try
                {
                    Console.WriteLine();
                    Console.Clear();
                    Tela.imprimirPartida(partida);

                    Console.WriteLine();
                    Console.Write("Digite a origem da peça : ");
                    Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                    partida.validarPosicaoOrigem(origem);

                    bool[,] posicoesPossiveis = partida.tab.peca(origem).movimentosPossiveis();

                    Console.Clear();
                    Tela.imprimirTabuleiro(partida.tab, posicoesPossiveis);

                    Console.WriteLine();
                    Console.Write("Digite o destino da peça : ");
                    Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
                    partida.validarPosicaoDestino(origem, destino);

                    partida.realizaJogada(origem, destino);
                }
                catch (TabuleiroExceptions e){
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }

            Console.Clear();
            Tela.imprimirPartida(partida);

        }
        catch (TabuleiroExceptions e)
        {
            Console.WriteLine(e.Message);
        }




    }
}