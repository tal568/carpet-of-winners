namespace carpet_of_winners.git
{
    internal class Player
    {
        public int Number { get; private set; }
        public int Col { get; private set; }
        public int Row { get; private set; }
        public Player(int col, int row, int number)
        {
            Number = number;
            Col = col;
            Row = row;

        }
        public void Move(int newCol, int newRow)
        {
            Row = newRow;
            Col = newCol;
        }
    }
}
