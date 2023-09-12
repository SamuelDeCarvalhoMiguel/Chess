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
      AvaibleToEnPassant = null;
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
    public Piece AvaibleToEnPassant { get; private set; }

    public Piece MakeAMove(Position origin, Position destination)
    {
      Piece piece = MatchBoard.RemovePiece(origin);
      piece.AddAmountOfMoves();
      Piece capturedPiece = MatchBoard.RemovePiece(destination);
      EndGame = false;
      MatchBoard.MovePiece(piece, destination);
      if (capturedPiece != null)
        CapturedPieces.Add(capturedPiece);

      //Small Castle
      if (piece is King && destination.Column == origin.Column + 2)
      {
        Position rookOrigin = new Position(origin.Line, origin.Column + 3);
        Position rookDestination = new Position(origin.Line, origin.Column + 1);
        Piece rook = MatchBoard.RemovePiece(rookOrigin);
        rook.AddAmountOfMoves();
        MatchBoard.MovePiece(rook, rookDestination);
      }

      //Big Castle
      if (piece is King && destination.Column == origin.Column - 2)
      {
        Position rookOrigin = new Position(origin.Line, origin.Column - 4);
        Position rookDestination = new Position(origin.Line, origin.Column - 1);
        Piece rook = MatchBoard.RemovePiece(rookOrigin);
        rook.AddAmountOfMoves();
        MatchBoard.MovePiece(rook, rookDestination);
      }

      //En Passant
      if (piece is Pawn)
      {
        if (origin.Column !=  destination.Column && capturedPiece == null)
        {
          Position pawnPosition;
          if (piece.Color == Color.White)
            pawnPosition = new Position(destination.Line + 1, destination.Column);
          else
            pawnPosition = new Position(destination.Line - 1, destination.Column);
          capturedPiece = MatchBoard.RemovePiece(pawnPosition);
          CapturedPieces.Add(capturedPiece);
        }
      }

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

      //Small Castle
      if (piece is King && destination.Column == origin.Column + 2)
      {
        Position rookOrigin = new Position(origin.Line, origin.Column + 3);
        Position rookDestination = new Position(origin.Line, origin.Column + 1);
        Piece rook = MatchBoard.RemovePiece(rookDestination);
        rook.SubAmountOfMoves();
        MatchBoard.MovePiece(rook, rookOrigin);
      }

      //Big Castle
      if (piece is King && destination.Column == origin.Column - 2)
      {
        Position rookOrigin = new Position(origin.Line, origin.Column - 4);
        Position rookDestination = new Position(origin.Line, origin.Column - 1);
        Piece rook = MatchBoard.RemovePiece(rookDestination);
        rook.SubAmountOfMoves();
        MatchBoard.MovePiece(rook, rookOrigin);
      }

      //En Passant
      if (piece is Pawn)
      {
        if (origin.Column != destination.Column && capturedPiece == AvaibleToEnPassant)
        {
          Piece pawn = MatchBoard.RemovePiece(destination);
          Position pawnPosition;
          if (pawn.Color == Color.White)
            pawnPosition = new Position(3, destination.Column);
          else
            pawnPosition = new Position(4, destination.Column);
          MatchBoard.MovePiece(pawn, pawnPosition);
        }
      }
    }

    public void PeformsAPlay(Position origin, Position destination)
    {
      Piece capturedPiece = MakeAMove(origin, destination);

      if (CheckTest(CurrentPlayer))
      {
        UndoAMove(origin, destination, capturedPiece);
        throw new BoardException("You can't put yourself in check!");
      }

      if (CheckTest(Adversary(CurrentPlayer)))
        Check = true;
      else
        Check = false;

      if (CheckmateTest(Adversary(CurrentPlayer)))
        EndGame = true;
      else
      {
        Turn++;
        ChangePlayersTurn();
      }

      Piece movedPiece = MatchBoard.ValidatePiecePositionUsingObject(destination);

      //En Passant
      if (movedPiece is Pawn && (destination.Line == origin.Line - 2 || destination.Line == origin.Line + 2))
        AvaibleToEnPassant = movedPiece;
      else
        AvaibleToEnPassant = null;
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
      if (!MatchBoard.ValidatePiecePositionUsingObject(origin).PieceCanMoveToThisDestination(destination))
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

    public bool CheckTest(Color color)
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

    public bool CheckmateTest(Color color)
    {
      if (!CheckTest(color))
        return false;

      foreach (Piece piece in PiecesInGame(color))
      {
        bool[,] matrice = piece.VerifyPossibleMoves();
        for (int i = 0; i < MatchBoard.Lines; i++)
        {
          for (int j = 0; j < MatchBoard.Columns; j++)
          {
            if (matrice[i, j])
            {
              Position origin = piece.Position;
              Position destination = new Position(i, j);
              Piece capturedPiece = MakeAMove(origin, destination);
              bool checkTest = CheckTest(color);
              UndoAMove(origin, destination, capturedPiece);

              if (!checkTest)
                return false;
            }
          }
        }
      }
      return true;
    }

    public void PlaceNewPiece(char column, int line, Piece piece)
    {
      MatchBoard.MovePiece(piece, new ChessPosition(column, line).ToPosition());
      Pieces.Add(piece);
    }

    private void PlacePiece()
    {
      PlaceNewPiece('a', 1, new Rook(MatchBoard, Color.White));
      PlaceNewPiece('b', 1, new Knight(MatchBoard, Color.White));
      PlaceNewPiece('c', 1, new Bishop(MatchBoard, Color.White));
      PlaceNewPiece('d', 1, new Queen(MatchBoard, Color.White));
      PlaceNewPiece('e', 1, new King(MatchBoard, Color.White, this));
      PlaceNewPiece('f', 1, new Bishop(MatchBoard, Color.White));
      PlaceNewPiece('g', 1, new Knight(MatchBoard, Color.White));
      PlaceNewPiece('h', 1, new Rook(MatchBoard, Color.White));
      PlaceNewPiece('a', 2, new Pawn(MatchBoard, Color.White, this));
      PlaceNewPiece('b', 2, new Pawn(MatchBoard, Color.White, this));
      PlaceNewPiece('c', 2, new Pawn(MatchBoard, Color.White, this));
      PlaceNewPiece('d', 2, new Pawn(MatchBoard, Color.White, this));
      PlaceNewPiece('e', 2, new Pawn(MatchBoard, Color.White, this));
      PlaceNewPiece('f', 2, new Pawn(MatchBoard, Color.White, this));
      PlaceNewPiece('g', 2, new Pawn(MatchBoard, Color.White, this));
      PlaceNewPiece('h', 2, new Pawn(MatchBoard, Color.White, this));

      PlaceNewPiece('a', 8, new Rook(MatchBoard, Color.Black));
      PlaceNewPiece('b', 8, new Knight(MatchBoard, Color.Black));
      PlaceNewPiece('c', 8, new Bishop(MatchBoard, Color.Black));
      PlaceNewPiece('d', 8, new Queen(MatchBoard, Color.Black));
      PlaceNewPiece('e', 8, new King(MatchBoard, Color.Black, this));
      PlaceNewPiece('f', 8, new Bishop(MatchBoard, Color.Black));
      PlaceNewPiece('g', 8, new Knight(MatchBoard, Color.Black));
      PlaceNewPiece('h', 8, new Rook(MatchBoard, Color.Black));
      PlaceNewPiece('a', 7, new Pawn(MatchBoard, Color.Black, this));
      PlaceNewPiece('b', 7, new Pawn(MatchBoard, Color.Black, this));
      PlaceNewPiece('c', 7, new Pawn(MatchBoard, Color.Black, this));
      PlaceNewPiece('d', 7, new Pawn(MatchBoard, Color.Black, this));
      PlaceNewPiece('e', 7, new Pawn(MatchBoard, Color.Black, this));
      PlaceNewPiece('f', 7, new Pawn(MatchBoard, Color.Black, this));
      PlaceNewPiece('g', 7, new Pawn(MatchBoard, Color.Black, this));
      PlaceNewPiece('h', 7, new Pawn(MatchBoard, Color.Black, this));

    }
  }
}