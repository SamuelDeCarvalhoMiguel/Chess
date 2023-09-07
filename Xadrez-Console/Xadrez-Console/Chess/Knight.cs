using Board;
using System.Reflection.Metadata.Ecma335;

namespace Chess
{
  class Knight : Piece 
  {
    public Knight(GameBoard board, Color color) : base (board, color) { }

    public override string ToString()
    {
      return "N";
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

      position.DefineValuesForThisPosition(Position.Line - 1, Position.Column - 2);
      if (Board.AvailablePosition(position) && VerifyIfThePieceCanMove(position))
        boardHouses[position.Line, position.Column] = true;

      //NorthEast
      position.DefineValuesForThisPosition(Position.Line - 2, Position.Column - 1);
      if (Board.AvailablePosition(position) && VerifyIfThePieceCanMove(position))
        boardHouses[position.Line, position.Column] = true;

      //East
      position.DefineValuesForThisPosition(Position.Line - 2, Position.Column + 1);
      if (Board.AvailablePosition(position) && VerifyIfThePieceCanMove(position))
        boardHouses[position.Line, position.Column] = true;

      //SouthEast
      position.DefineValuesForThisPosition(Position.Line - 1, Position.Column + 2);
      if (Board.AvailablePosition(position) && VerifyIfThePieceCanMove(position))
        boardHouses[position.Line, position.Column] = true;

      //South
      position.DefineValuesForThisPosition(Position.Line + 1, Position.Column + 2);
      if (Board.AvailablePosition(position) && VerifyIfThePieceCanMove(position))
        boardHouses[position.Line, position.Column] = true;

      //SouthWest
      position.DefineValuesForThisPosition(Position.Line + 2, Position.Column + 1);
      if (Board.AvailablePosition(position) && VerifyIfThePieceCanMove(position))
        boardHouses[position.Line, position.Column] = true;

      //West
      position.DefineValuesForThisPosition(Position.Line + 2, Position.Column - 1);
      if (Board.AvailablePosition(position) && VerifyIfThePieceCanMove(position))
        boardHouses[position.Line, position.Column] = true;

      //NorthWest
      position.DefineValuesForThisPosition(Position.Line + 1, Position.Column - 2);
      if (Board.AvailablePosition(position) && VerifyIfThePieceCanMove(position))
        boardHouses[position.Line, position.Column] = true;

      return boardHouses;
    }
  }
}
