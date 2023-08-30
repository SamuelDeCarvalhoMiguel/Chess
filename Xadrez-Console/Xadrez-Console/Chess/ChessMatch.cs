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
    private int Turn;
    private Color CurrentPlayer;
    public bool EndGame { get; private set; }

    public void MakeAMove(Position origin, Position destination)
    {
      Piece piece = MatchBoard.RemovePiece(origin);
      piece.AddAmountOfMoves();
      Piece CapturedPiece = MatchBoard.RemovePiece(destination);
      EndGame = false;
      MatchBoard.MovePiece(piece, destination);
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
