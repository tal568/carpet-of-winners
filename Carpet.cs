public class Carpet
{
    public int TopLeftRow { get; set; }
    public int TopLeftCol { get; set; }
    public int BottomRightRow { get; set; }
    public int BottomRightCol { get; set; }

    public Carpet(int topLeftRow, int topLeftCol, int size)
    {
        TopLeftRow = topLeftRow;
        TopLeftCol = topLeftCol;
        BottomRightRow = topLeftRow + size;
        BottomRightCol = topLeftCol + size;
    }

    public bool Contains(int row, int col)
    {
        return row >= TopLeftRow
            && row <= BottomRightRow
            && col >= TopLeftCol
            && col <= BottomRightCol;
    }
}