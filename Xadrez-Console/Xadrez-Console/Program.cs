using System;
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
        ChessMatch match = new ChessMatch();

        while (!match.EndGame)
        {

          Console.Clear();
          Screen.PrintBoard(match.MatchBoard);

          Console.WriteLine();
          Console.Write("Origin: ");
          Position origin = Screen.ReadPiecePosition().ToPosition();

          bool[,] possiblePositions = match.MatchBoard.ValidatePiecePositionUsingObject(origin).VerifyPossibleMoves();

          Console.Clear();
          Screen.PrintBoard(match.MatchBoard,possiblePositions);

          Console.WriteLine();
          Console.Write("Destination: ");
          Position destination = Screen.ReadPiecePosition().ToPosition();

          match.MakeAMove(origin, destination);
        }
      }
      catch (Exception exception)
      {
        Console.WriteLine(exception.Message);
      }
    }
  }
}