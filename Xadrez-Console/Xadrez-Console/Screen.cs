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
                for (int j = 0; j < board.Columns; j++)
                {
                    if (board.PiecePositionUsingCoordinates(i, j) == null)
                        Console.Write("- ");
                    else
                        Console.Write(board.PiecePositionUsingCoordinates(i, j) + " ");
                }

                Console.WriteLine();
            }
        }

    }
}
