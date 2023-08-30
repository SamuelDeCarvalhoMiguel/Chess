namespace Board
{
  class Piece
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

  }
}
