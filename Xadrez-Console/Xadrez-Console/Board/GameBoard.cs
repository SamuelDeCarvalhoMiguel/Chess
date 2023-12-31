﻿namespace Board
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

    public Piece ValidatePiecePositionUsingCoordinates(int line, int column)
    {
      return Pieces[line, column];
    }
    public Piece ValidatePiecePositionUsingObject(Position position)
    {
      return Pieces[position.Line, position.Column];
    }

    public bool VerifyIfPieceExist(Position position)
    {
      ValidatePosition(position);
      return ValidatePiecePositionUsingObject(position) != null;
    }

    public void MovePiece(Piece piece, Position position)
    {
      if (VerifyIfPieceExist(position))
        throw new BoardException("There is already a piece in this position!");

      Pieces[position.Line, position.Column] = piece;
      piece.Position = position;
    }

    public Piece RemovePiece(Position position)
    {
      if (ValidatePiecePositionUsingObject(position) == null)
        return null;

      Piece pieceRemover = ValidatePiecePositionUsingObject(position);
      pieceRemover.Position = null;
      Pieces[position.Line, position.Column] = null;
      return pieceRemover;
    }

    public bool AvailablePosition(Position position)
    {
      if (position.Line < 0 || position.Line >= Lines || position.Column < 0 || position.Column >= Columns)
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
