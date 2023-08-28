using System;
using Board;

namespace Xadrez_Console
{
  class Screen
  {

    public static void PrintBoard(GameBoard board)
    {
      for (int i = 0; i < board.Lines; i++)
      {
        Console.Write(8 - i + " ");
        for (int j = 0; j < board.Columns; j++)
        {
          if (board.PiecePositionUsingCoordinates(i, j) == null)
            Console.Write("- ");
          else
          {
            Screen.PrintPiece(board.PiecePositionUsingCoordinates((int)i, (int)j));
            Console.Write(" ");
          }
        }

        Console.WriteLine();
      }
      Console.WriteLine("  A B C D E F G H");
    }

    public static void PrintPiece(Piece piece)
    {
      if (piece.Color == Color.White)
        Console.Write(piece);

      else
      {
        ConsoleColor aux = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(piece);
        Console.ForegroundColor = aux;
      }

    }

  }
}
