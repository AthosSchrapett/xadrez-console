﻿using xadrez_console;
using xadrez_console.Tabuleiro;
using xadrez_console.Tabuleiro.Enum;
using xadrez_console.Tabuleiro.Exceptions;
using xadrez_console.Xadrez;

//try
//{
//    TabuleiroTab tab = new(8, 8);

//    tab.ColocarPeca(new Torre(Cor.Preta, tab), new Posicao(0, 0));
//    tab.ColocarPeca(new Torre(Cor.Preta, tab), new Posicao(1, 3));
//    tab.ColocarPeca(new Rei(Cor.Preta, tab), new Posicao(2, 4));

//    Tela.ImprimirTabuleiro(tab);
//}
//catch (TabuleiroException e)
//{
//    Console.WriteLine(e.Message);
//}

PosicaoXadrez posicaoXadrez = new('c', 7);

Console.WriteLine(posicaoXadrez);
Console.WriteLine(posicaoXadrez.ToPosicao());

Console.ReadLine();