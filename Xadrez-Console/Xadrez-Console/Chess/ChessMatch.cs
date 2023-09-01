using Board;
using System.Runtime.CompilerServices;

namespace Chess
{
  class ChessMatch
  {

    public ChessMatch() 
    {
      MatchBoard = new GameBoard(8, 8);
      Turn = 1;
      CurrentPlayer = Color.White;
      PlacePiece();
    }

    public GameBoard MatchBoard { get; private set; }
    public int Turn { get; private set; }
    public Color CurrentPlayer { get; private set; }
    public bool EndGame { get; private set; }

    public void MakeAMove(Position origin, Position destination)
    {
      Piece piece = MatchBoard.RemovePiece(origin);
      piece.AddAmountOfMoves();
      Piece CapturedPiece = MatchBoard.RemovePiece(destination);
      EndGame = false;
      MatchBoard.MovePiece(piece, destination);
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

    private void PlacePiece()
    {

      MatchBoard.MovePiece(new Rook(MatchBoard, Color.White), new ChessPosition('c', 1).ToPosition());
      MatchBoard.MovePiece(new Rook(MatchBoard, Color.White), new ChessPosition('c', 2).ToPosition());
      MatchBoard.MovePiece(new Rook(MatchBoard, Color.White), new ChessPosition('d', 2).ToPosition());
      MatchBoard.MovePiece(new Rook(MatchBoard, Color.White), new ChessPosition('e', 2).ToPosition());
      MatchBoard.MovePiece(new Rook(MatchBoard, Color.White), new ChessPosition('e', 1).ToPosition());
      MatchBoard.MovePiece(new King(MatchBoard, Color.White), new ChessPosition('d', 1).ToPosition());

      MatchBoard.MovePiece(new Rook(MatchBoard, Color.Black), new ChessPosition('c', 7).ToPosition());
      MatchBoard.MovePiece(new Rook(MatchBoard, Color.Black), new ChessPosition('c', 8).ToPosition());
      MatchBoard.MovePiece(new Rook(MatchBoard, Color.Black), new ChessPosition('d', 7).ToPosition());
      MatchBoard.MovePiece(new Rook(MatchBoard, Color.Black), new ChessPosition('e', 8).ToPosition());
      MatchBoard.MovePiece(new Rook(MatchBoard, Color.Black), new ChessPosition('e', 7).ToPosition());
      MatchBoard.MovePiece(new King(MatchBoard, Color.Black), new ChessPosition('d', 8).ToPosition());

    }
  }
}
