namespace Board
{
  abstract class Piece
  {

    public Piece() { }
    public Piece(GameBoard board, Color color)
    {
      Position = null;
      Color = color;
      Board = board;
    }

    public Position Position { get; set; }
    public Color Color { get; protected set; }
    public int AmountOfMoves { get; protected set; }
    public GameBoard Board { get; protected set; }

    public void AddAmountOfMoves()
    {
      AmountOfMoves++;
    }

    public void SubAmountOfMoves()
    {
      AmountOfMoves--;
    }

    public bool VerifyIfExistAPossibleMove()
    {
      bool[,] possibleMovesMatrices = VerifyPossibleMoves();
      for (int i = 0; i < Board.Lines; i++)
      {
        for (int j = 0; j < Board.Lines; j++)
        {
          if (possibleMovesMatrices[i, j])
            return true;
        }
      }
      return false;
    }

    public bool PieceCanMoveToThisPosition(Position position)
    {
      return VerifyPossibleMoves()[position.Line, position.Column];
    }
    public abstract bool[,] VerifyPossibleMoves();
  }
}
