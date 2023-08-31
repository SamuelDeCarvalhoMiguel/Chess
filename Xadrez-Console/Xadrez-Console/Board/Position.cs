namespace Board
{
  class Position
  {

    public Position() { }
    public Position(int line, int column)
    {
      Line = line;
      Column = column;
    }

    public int Line { get; set; }
    public int Column { get; set; }

    public override string ToString()
    {
      return $"Line: {Line}, Column: {Column}";
    }

    public void DefineValuesForThisPosition(int line, int column)
    {
      Line = line;
      Column = column;
    }

  }
}
