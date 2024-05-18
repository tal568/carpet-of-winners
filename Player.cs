namespace carpet_of_winners.git;

internal class Player
{
    public int Number { get; private set; }
    public int Col { get; private set; }
    public int Row { get; private set; }

    public Player(int row, int col, int number)
    {
        Number = number;
        Col = col-1;
        Row = row-1;
    }

    public void Move(int newRow, int newCol)
    {
        Row = newRow;
        Col = newCol;
    }
}
