using System;
using Board;
using Chess;
using System.Collections.Generic;

namespace Xadrez_Console
{
  class Screen
  {

    public static void PrintMatch(ChessMatch match)
    {
      PrintBoard(match.MatchBoard);
      Console.WriteLine();
      PrintCapturedPieces(match);
      Console.WriteLine();
      Console.WriteLine($"Turn: {match.Turn}");

      if (!match.EndGame)
      {
        Console.WriteLine($"Waiting for a move: {match.CurrentPlayer}");

        if (match.Check)
          Console.WriteLine("CHEK!");
      }
      else
      {
        Console.WriteLine("CHEKMATE!");
        Console.WriteLine($"Winner: {match.CurrentPlayer}");
      }
    }

    public static void PrintCapturedPieces(ChessMatch match)
    {
      Console.WriteLine("Captured pieces: ");
      Console.Write("White: ");
      PrintSet(match.CapturedPiecesSet(Color.White));
      Console.Write("Black: ");

      ConsoleColor standartColor = Console.ForegroundColor;
      Console.ForegroundColor = ConsoleColor.Yellow;
      PrintSet(match.CapturedPiecesSet(Color.Black));
      Console.ForegroundColor = standartColor;
    }

    public static void PrintSet(HashSet<Piece> set)
    {
      Console.Write("[");
      foreach (Piece piece in set)
      {
        Console.Write(piece + " ");
      }
      Console.WriteLine("]");
    }

    public static void PrintBoard(GameBoard board)
    {
      for (int i = 0; i < board.Lines; i++)
      {
        Console.Write(8 - i + " ");
        for (int j = 0; j < board.Columns; j++)
        {
          Screen.PrintPiece(board.ValidatePiecePositionUsingCoordinates((int)i, (int)j));
        }
        Console.WriteLine();
      }
      Console.WriteLine("  A B C D E F G H");
    }

    public static void PrintBoard(GameBoard board, bool[,] possiblePositions)
    {

      ConsoleColor originalBackGround = Console.BackgroundColor;
      ConsoleColor alternativeBackGround = ConsoleColor.DarkGray;

      for (int i = 0; i < board.Lines; i++)
      {
        Console.Write(8 - i + " ");
        for (int j = 0; j < board.Columns; j++)
        {
          if (possiblePositions[i, j])
            Console.BackgroundColor = alternativeBackGround;
          else
            Console.BackgroundColor = originalBackGround;

          Screen.PrintPiece(board.ValidatePiecePositionUsingCoordinates((int)i, (int)j));
          Console.BackgroundColor = originalBackGround;
        }
        Console.WriteLine();
      }
      Console.WriteLine("  A B C D E F G H");
      Console.BackgroundColor = originalBackGround;
    }

    public static ChessPosition ReadPiecePosition()
    {

      string selectedPosition = Console.ReadLine();

      char Column = selectedPosition[0];
      int Line = int.Parse(selectedPosition[1] + "");

      return new ChessPosition(Column, Line);
    }

    public static void PrintPiece(Piece piece)
    {

      if (piece == null)
      {
        Console.Write("- ");
      }

      else
      {
        if (piece.Color == Color.White)
          Console.Write(piece);
        else
        {
          ConsoleColor standartColor = Console.ForegroundColor;
          Console.ForegroundColor = ConsoleColor.Yellow;
          Console.Write(piece);
          Console.ForegroundColor = standartColor;
        }
        Console.Write(" ");
      }
    }
  }
}
