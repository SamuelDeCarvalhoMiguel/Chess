using Board;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace Chess
{
  class ChessMatch
  {

    public ChessMatch()
    {
      MatchBoard = new GameBoard(8, 8);
      Turn = 1;
      CurrentPlayer = Color.White;
      EndGame = false;
      Pieces = new HashSet<Piece>();
      CapturedPieces = new HashSet<Piece>();
      Check = false;
      PlacePiece();
    }

    public GameBoard MatchBoard { get; private set; }
    public int Turn { get; private set; }
    public Color CurrentPlayer { get; private set; }
    public bool EndGame { get; private set; }
    private HashSet<Piece> Pieces;
    private HashSet<Piece> CapturedPieces;
    public bool Check { get; private set; }

    public Piece MakeAMove(Position origin, Position destination)
    {
      Piece piece = MatchBoard.RemovePiece(origin);
      piece.AddAmountOfMoves();
      Piece capturedPiece = MatchBoard.RemovePiece(destination);
      EndGame = false;
      MatchBoard.MovePiece(piece, destination);
      if (capturedPiece != null)
        CapturedPieces.Add(capturedPiece);
      return capturedPiece;
    }

    public void UndoAMove(Position origin, Position destination, Piece capturedPiece)
    {
      Piece piece = MatchBoard.RemovePiece(destination);
      piece.SubAmountOfMoves();
      if (capturedPiece != null)
      {
        MatchBoard.MovePiece(capturedPiece, destination);
        CapturedPieces.Remove(capturedPiece);
      }
      MatchBoard.MovePiece(piece, origin);
    }

    public void PeformsAMove(Position origin, Position destination)
    {
      Piece capturedPiece = MakeAMove(origin, destination);

      if (IsInCheck(CurrentPlayer))
      {
        UndoAMove(origin, destination, capturedPiece);
        throw new BoardException("You can't put yourself in check!");
      }

      if (IsInCheck(Adversary(CurrentPlayer)))
        Check = true;
      else
        Check = false;

      Turn++;
      ChangePlayersTurn();
    }

    public void ValidateOriginPosition(Position position)
    {
      if (MatchBoard.ValidatePiecePositionUsingObject(position) == null)
        throw new BoardException("There is not a piece in this position!");
      if (CurrentPlayer != MatchBoard.ValidatePiecePositionUsingObject(position).Color)
        throw new BoardException("This piece is not yours!");
      if (!MatchBoard.ValidatePiecePositionUsingObject(position).VerifyIfExistAPossibleMove())
        throw new BoardException("There is not a possible move for this piece!");
    }

    public void ValidateDestinationPosition(Position origin, Position destination)
    {
      if (!MatchBoard.ValidatePiecePositionUsingObject(origin).PieceCanMoveToThisPosition(destination))
        throw new BoardException("Invalid destination position!");

    }

    private void ChangePlayersTurn()
    {
      if (CurrentPlayer == Color.White)
        CurrentPlayer = Color.Black;
      else
        CurrentPlayer = Color.White;
    }

    public HashSet<Piece> CapturedPiecesSet(Color color)
    {
      HashSet<Piece> capturedPieceColor = new HashSet<Piece>();
      foreach (Piece piece in CapturedPieces)
      {
        if (piece.Color == color)
          capturedPieceColor.Add(piece);
      }
      return capturedPieceColor;
    }

    public HashSet<Piece> PiecesInGame(Color color)
    {
      HashSet<Piece> pieceSet = new HashSet<Piece>();
      foreach (Piece piece in Pieces)
      {
        if (piece.Color == color)
          pieceSet.Add(piece);
      }
      pieceSet.ExceptWith(CapturedPiecesSet(color));
      return pieceSet;
    }

    private Color Adversary(Color color)
    {
      if (color == Color.White)
        return Color.Black;
      else
        return Color.White;
    }

    private Piece VerifyIfPieceIsKing(Color color)
    {
      foreach (Piece piece in PiecesInGame(color))
      {
        if (piece is King)
        {
          return piece;
        }
      }
      return null;
    }

    public bool IsInCheck(Color color)
    {
      Piece king = VerifyIfPieceIsKing(color);
      if (king == null)
      {
        throw new BoardException($"There is no {color} king in the board!");
      }

      foreach (Piece piece in PiecesInGame(Adversary(color)))
      {
        bool[,] matrices = piece.VerifyPossibleMoves();
        if (matrices[king.Position.Line, king.Position.Column])
          return true;
      }
      return false;
    }

    public void PlaceNewPiece(char column, int line, Piece piece)
    {
      MatchBoard.MovePiece(piece, new ChessPosition(column, line).ToPosition());
      Pieces.Add(piece);
    }

    private void PlacePiece()
    {
      PlaceNewPiece('c', 1, new Rook(MatchBoard, Color.White));
      PlaceNewPiece('c', 2, new Rook(MatchBoard, Color.White));
      PlaceNewPiece('d', 2, new Rook(MatchBoard, Color.White));
      PlaceNewPiece('e', 2, new Rook(MatchBoard, Color.White));
      PlaceNewPiece('e', 1, new Rook(MatchBoard, Color.White));
      PlaceNewPiece('d', 1, new King(MatchBoard, Color.White));

      PlaceNewPiece('c', 7, new Rook(MatchBoard, Color.Black));
      PlaceNewPiece('c', 8, new Rook(MatchBoard, Color.Black));
      PlaceNewPiece('d', 7, new Rook(MatchBoard, Color.Black));
      PlaceNewPiece('e', 8, new Rook(MatchBoard, Color.Black));
      PlaceNewPiece('e', 7, new Rook(MatchBoard, Color.Black));
      PlaceNewPiece('d', 8, new King(MatchBoard, Color.Black));

    }
  }
}
