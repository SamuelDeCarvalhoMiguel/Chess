using Board;
using System.Reflection.Metadata.Ecma335;

namespace Chess
{
  class Pawn : Piece 
  {
    public Pawn(GameBoard board, Color color) : base (board, color) { }

    public override string ToString()
    {
      return "P";
    }

    private bool EnemyExist(Position position)
    {
      Piece piece = Board.ValidatePiecePositionUsingObject(position);
      return piece != null && piece.Color != Color;
    }

    private bool EmptyPosition(Position position)
    {
      return Board.ValidatePiecePositionUsingObject(position) == null;
    }

    public override bool[,] VerifyPossibleMoves()
    {
      bool[,] boardHouses = new bool[Board.Lines, Board.Columns];

      Position position = new Position(0, 0);

      if (Color == Color.White)
      {
        position.DefineValuesForThisPosition(Position.Line - 1, Position.Column);
        if (Board.AvailablePosition(position) && EmptyPosition(position))
          boardHouses[position.Line, position.Column] = true;

        position.DefineValuesForThisPosition(Position.Line - 2, Position.Column);
        if (Board.AvailablePosition(position) && EmptyPosition(position) && AmountOfMoves == 0)
          boardHouses[position.Line, position.Column] = true;

        position.DefineValuesForThisPosition(Position.Line - 1, Position.Column - 1);
        if (Board.AvailablePosition(position) && EnemyExist(position))
          boardHouses[position.Line, position.Column] = true;

        position.DefineValuesForThisPosition(Position.Line - 1, Position.Column + 1);
        if (Board.AvailablePosition(position) && EnemyExist(position))
          boardHouses[position.Line, position.Column] = true;
      }

      else if (Color == Color.Black)
      {
        position.DefineValuesForThisPosition(Position.Line + 1, Position.Column);
        if (Board.AvailablePosition(position) && EmptyPosition(position))
          boardHouses[position.Line, position.Column] = true;

        position.DefineValuesForThisPosition(Position.Line + 2, Position.Column);
        if (Board.AvailablePosition(position) && EmptyPosition(position) && AmountOfMoves == 0)
          boardHouses[position.Line, position.Column] = true;

        position.DefineValuesForThisPosition(Position.Line + 1, Position.Column + 1);
        if (Board.AvailablePosition(position) && EnemyExist(position))
          boardHouses[position.Line, position.Column] = true;

        position.DefineValuesForThisPosition(Position.Line + 1, Position.Column - 1);
        if (Board.AvailablePosition(position) && EnemyExist(position))
          boardHouses[position.Line, position.Column] = true;
      }

      return boardHouses;
    }
  }
}
