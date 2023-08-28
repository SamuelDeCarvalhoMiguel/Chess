﻿using System;
using Board;
using Chess;

namespace Xadrez_Console
{
  class Program
  {
    static void Main(string[] args)
    {
      try
      {
        GameBoard board = new GameBoard(8, 8);

        board.PlacePiece(new Rook(board, Color.Black), new Position(0, 0));
        board.PlacePiece(new Rook(board, Color.Black), new Position(1, 3));
        board.PlacePiece(new King(board, Color.Black), new Position(0, 3));

        board.PlacePiece(new Rook(board, Color.White), new Position(3, 5));

        Screen.PrintBoard(board);
      }
      catch (Exception exception)
      {
        Console.WriteLine(exception.Message);
      }
    }
  }
}