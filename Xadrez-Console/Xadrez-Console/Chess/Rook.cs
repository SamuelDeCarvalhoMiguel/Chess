using Board;
using System.Runtime.ConstrainedExecution;

namespace Chess
{
  class Rook : Piece
  {
    public Rook(GameBoard board, Color color) : base(board, color) { }

    public override string ToString()
    {
      return "R";
    }

    private bool VerifyIfThePieceCanMove(Position position)
    {
      Piece piece = Board.ValidatePiecePositionUsingObject(position);
      return piece == null || piece.Color != Color;
    }

    public override bool[,] VerifyPossibleMoves()
    {
      bool[,] boardHouses = new bool[Board.Lines, Board.Columns];

      Position position = new Position(0, 0);

      //North
      position.DefineValuesForThisPosition(Position.Line - 1, Position.Column);
      while (Board.AvailablePosition(position) && VerifyIfThePieceCanMove(position))
      {
        boardHouses[position.Line, position.Column] = true;
        if (Board.ValidatePiecePositionUsingObject(position) != null && Board.ValidatePiecePositionUsingObject(position).Color != Color)
        {
          break;
        }
        position.Line = position.Line - 1;
      }

      //South
      position.DefineValuesForThisPosition(Position.Line + 1, Position.Column);
      while (Board.AvailablePosition(position) && VerifyIfThePieceCanMove(position))
      {
        boardHouses[position.Line, position.Column] = true;
        if (Board.ValidatePiecePositionUsingObject(position) != null && Board.ValidatePiecePositionUsingObject(position).Color != Color)
        {
          break;
        }
        position.Line = position.Line + 1;
      }

      //East
      position.DefineValuesForThisPosition(Position.Line, Position.Column + 1);
      while (Board.AvailablePosition(position) && VerifyIfThePieceCanMove(position))
      {
        boardHouses[position.Line, position.Column] = true;
        if (Board.ValidatePiecePositionUsingObject(position) != null && Board.ValidatePiecePositionUsingObject(position).Color != Color)
        {
          break;
        }
        position.Column = position.Column + 1;
      }

      //West
      position.DefineValuesForThisPosition(Position.Line, Position.Column - 1);
      while (Board.AvailablePosition(position) && VerifyIfThePieceCanMove(position))
      {
        boardHouses[position.Line, position.Column] = true;
        if (Board.ValidatePiecePositionUsingObject(position) != null && Board.ValidatePiecePositionUsingObject(position).Color != Color)
        {
          break;
        }
        position.Column = position.Column - 1;
      }

      return boardHouses;
    }

  }
}
