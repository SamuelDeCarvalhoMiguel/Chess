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

          try
          {
            Console.Clear();
            Screen.PrintMatch(match);

            Console.WriteLine();
            Console.Write("Origin: ");
            Position origin = Screen.ReadPiecePosition().ToPosition();
            match.ValidateOriginPosition(origin);

            bool[,] possiblePositions = match.MatchBoard.ValidatePiecePositionUsingObject(origin).VerifyPossibleMoves();

            Console.Clear();
            Screen.PrintBoard(match.MatchBoard, possiblePositions);

            Console.WriteLine();
            Console.Write("Destination: ");
            Position destination = Screen.ReadPiecePosition().ToPosition();
            match.ValidateDestinationPosition(origin, destination);

            match.PeformsAPlay(origin, destination);
          }
          catch (BoardException exception)
          {
            Console.WriteLine(exception.Message);
            Console.ReadLine();
          }
        }
        Console.Clear();
        Screen.PrintMatch(match);
      }
      catch (Exception exception)
      {
        Console.WriteLine(exception.Message);
      }
    }
  }
}