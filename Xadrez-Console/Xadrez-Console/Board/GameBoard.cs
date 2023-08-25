namespace Board
{
  class GameBoard
  {
    public GameBoard(int lines, int columns)
    {
      Lines = lines;
      Columns = columns;
      Pieces = new Piece[lines, columns];
    }

    public int Lines { get; set; }
    public int Columns { get; set; }
    private Piece[,] Pieces;

    public Piece PiecePositionUsingCoordinates(int line, int column)
    {
      return Pieces[line, column];
    }
    public Piece PiecePositionUsingObject(Position position)
    {
      return Pieces[position.Line, position.Column];
    }

    public bool VerifyIfPieceExist(Position position)
    {
      ValidatePosition(position);
      return PiecePositionUsingObject(position) != null;
    }

    public void PlacePiece(Piece piece, Position position)
    {
      if (VerifyIfPieceExist(position))
        throw new BoardException("There is already a piece in this position!");

      Pieces[position.Line, position.Column] = piece;
      piece.Position = position;
    }

    public bool AvailablePosition(Position position)
    {
      if (position.Line < 0 || position.Line > Lines || position.Column < 0 || position.Column > Columns)
        return false;
      return true;
    }

    public void ValidatePosition(Position position)
    {
      if (!AvailablePosition(position))
        throw new BoardException("Invalid Position!");
    }
  }
}
