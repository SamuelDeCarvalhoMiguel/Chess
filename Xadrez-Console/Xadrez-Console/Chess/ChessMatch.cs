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
      PlacePiece();
    }

    public GameBoard MatchBoard { get; private set; }
    public int Turn { get; private set; }
    public Color CurrentPlayer { get; private set; }
    public bool EndGame { get; private set; }
    private HashSet<Piece> Pieces;
    private HashSet<Piece> CapturedPieces;

    public void MakeAMove(Position origin, Position destination)
    {
      Piece piece = MatchBoard.RemovePiece(origin);
      piece.AddAmountOfMoves();
      Piece CapturedPiece = MatchBoard.RemovePiece(destination);
      EndGame = false;
      MatchBoard.MovePiece(piece, destination);
      if (CapturedPiece != null)
        CapturedPieces.Add(CapturedPiece);
    }

    public void PeformsAMove(Position origin, Position destination)
    {
      MakeAMove(origin, destination);
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
      HashSet<Piece> capturedPiece = new HashSet<Piece>();
      foreach (Piece piece in CapturedPieces)
      {
        if (piece.Color == color)
          capturedPiece.Add(piece);
      }
      capturedPiece.ExceptWith(CapturedPiecesSet(color));
      return capturedPiece;
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
