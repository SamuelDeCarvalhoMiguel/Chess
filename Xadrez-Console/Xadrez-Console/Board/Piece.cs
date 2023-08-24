namespace Board
{
    class Piece
    {

        public Piece() { }
        public Piece(Position position, Color color, GameBoard board)
        {
            Position = position;
            Color = color;
            Board = board;
        }

        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int AmountOfMoves { get; protected set; }
        public GameBoard Board { get; protected set; }  

    }
}
