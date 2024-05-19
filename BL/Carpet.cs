public class Carpet
{
    public int TopLeftRow { get; set; }
    public int TopLeftCol { get; set; }
    public int BottomRightRow { get; set; }
    public int BottomRightCol { get; set; }

    public Carpet(int topLeftRow, int topLeftCol, int size)
    {
        TopLeftRow = topLeftRow - 1;
        TopLeftCol = topLeftCol - 1;
        BottomRightRow = topLeftRow - 1 + size - 1;
        BottomRightCol = topLeftCol - 1 + size - 1;
    }

    public bool Contains(int row, int col)
    {
        return row >= TopLeftRow
            && row <= BottomRightRow
            && col >= TopLeftCol
            && col <= BottomRightCol;
    }
}
