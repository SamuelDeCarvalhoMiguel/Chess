using Board;
using System.Reflection.Metadata.Ecma335;

namespace Chess
{
  class King : Piece 
  {

    private ChessMatch Match;

    public King(GameBoard board, Color color, ChessMatch match) : base (board, color)
    {
      Match = match;
    }

    public override string ToString()
    {
      return "K";
    }

    private bool VerifyIfThePieceCanMove(Position position)
    {
      Piece piece = Board.ValidatePiecePositionUsingObject(position);
      return piece == null || piece.Color != Color;
    }

    private bool ValidateRookCastle(Position position)
    {
      Piece piece = Board.ValidatePiecePositionUsingObject(position);
      return piece != null && piece is Rook && piece.Color == Color && piece.AmountOfMoves == 0;
    }

    public override bool[,] VerifyPossibleMoves()
    {
      bool[,] boardHouses = new bool[Board.Lines, Board.Columns];

      Position position = new Position(0, 0);

      //North
      position.DefineValuesForThisPosition(Position.Line - 1, Position.Column);
      if (Board.AvailablePosition(position) && VerifyIfThePieceCanMove(position))
        boardHouses[position.Line, position.Column] = true;

      //NorthEast
      position.DefineValuesForThisPosition(Position.Line - 1, Position.Column + 1);
      if (Board.AvailablePosition(position) && VerifyIfThePieceCanMove(position))
        boardHouses[position.Line, position.Column] = true;

      //East
      position.DefineValuesForThisPosition(Position.Line, Position.Column + 1);
      if (Board.AvailablePosition(position) && VerifyIfThePieceCanMove(position))
        boardHouses[position.Line, position.Column] = true;

      //SouthEast
      position.DefineValuesForThisPosition(Position.Line + 1, Position.Column + 1);
      if (Board.AvailablePosition(position) && VerifyIfThePieceCanMove(position))
        boardHouses[position.Line, position.Column] = true;

      //South
      position.DefineValuesForThisPosition(Position.Line + 1, Position.Column);
      if (Board.AvailablePosition(position) && VerifyIfThePieceCanMove(position))
        boardHouses[position.Line, position.Column] = true;

      //SouthWest
      position.DefineValuesForThisPosition(Position.Line + 1, Position.Column - 1);
      if (Board.AvailablePosition(position) && VerifyIfThePieceCanMove(position))
        boardHouses[position.Line, position.Column] = true;

      //West
      position.DefineValuesForThisPosition(Position.Line, Position.Column - 1);
      if (Board.AvailablePosition(position) && VerifyIfThePieceCanMove(position))
        boardHouses[position.Line, position.Column] = true;

      //NorthWest
      position.DefineValuesForThisPosition(Position.Line - 1, Position.Column - 1);
      if (Board.AvailablePosition(position) && VerifyIfThePieceCanMove(position))
        boardHouses[position.Line, position.Column] = true;

      position.DefineValuesForThisPosition(Position.Line, Position.Column);

      //Castle
      if (AmountOfMoves == 0 && !Match.Check)
      {
        //Small Castle
        Position smallCastleRook = new Position(position.Line, position.Column + 3);

        if (ValidateRookCastle(smallCastleRook))
        {
          Position firstRookAndKingSpace = new Position(Position.Line, position.Column + 1);
          Position secondRookAndKingSpace = new Position(Position.Line, position.Column + 2);

          if (Board.ValidatePiecePositionUsingObject(firstRookAndKingSpace) == null && 
              Board.ValidatePiecePositionUsingObject(secondRookAndKingSpace) == null)
          {
            boardHouses[position.Line, position.Column + 2] = true;
          }
        }

        //Big Castle
        Position bigCastleRook = new Position(position.Line, position.Column - 4);

        if (ValidateRookCastle(bigCastleRook))
        {
          Position firstRookAndKingSpace = new Position(Position.Line, position.Column - 1);
          Position secondRookAndKingSpace = new Position(Position.Line, position.Column - 2);
          Position thirdRookAndKingSpace = new Position(Position.Line, position.Column - 3);

          if (Board.ValidatePiecePositionUsingObject(firstRookAndKingSpace) == null &&
              Board.ValidatePiecePositionUsingObject(secondRookAndKingSpace) == null &&
              Board.ValidatePiecePositionUsingObject(thirdRookAndKingSpace) == null)
          {
            boardHouses[position.Line, position.Column - 2] = true;
          }
        }
      }

      return boardHouses;
    }
  }
}
