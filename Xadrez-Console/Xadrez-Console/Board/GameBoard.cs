namespace Board
{
    class GameBoard
    {

        public GameBoard() { }
        public GameBoard(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            Pieces = new Piece[lines, columns];
        }

        public int Lines { get; set; }
        public int Columns { get; set; }
        private Piece[,] Pieces;


        public Piece PiecePosition(int line, int column)
        {
            return Pieces[line, column];
        }


    }
}
