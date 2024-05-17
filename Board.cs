using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace carpet_of_winners.git
{
    internal class Board
    {
        public int[,] grid;
        public List<Player> Players { get; private set; }
        public Carpet? Carpet { get; private set; }

        public Board(int cols ,int rows)
        {
            grid = new int[cols, rows];
           Carpet=null;
            Players = new List<Player>();
        }

        private void FillBoardWithCarpet()
        {
            if (Carpet == null)
            {
                throw new InvalidOperationException("carpet was not added to board");
            }
            for (int i = Carpet.TopLeftRow; i <= Carpet.BottomRightRow; i++)
            {
                for (int j = Carpet.TopLeftCol; j <= Carpet.BottomRightCol; j++)
                {
                    grid[i, j] = 2;
                }
            }
        }
        public string AddPlayer(Player player)
        {
            if (!IsWithinBounds(player.Row, player.Col))
                return "player is outside the board";
            if (IsWithinPlayer(player.Col,player.Row))
                return "there a player in that posion";
                Players.Add(player);
                grid[player.Row, player.Col] = player.Number;
                return ""; 
       
        }
        public bool IsWithinPlayer(int col,int row)
        {
            foreach(var playerOnBoard in Players)
            {
                if (playerOnBoard.Col == col && playerOnBoard.Row == row)
                    return true;
            }
            return false;
        }
        public string AddCarpet(Carpet carpet)
        {
            if (!IsWithinBounds(carpet.TopLeftRow, carpet.TopLeftCol) || !IsWithinBounds(carpet.BottomRightRow, carpet.BottomRightCol))
                return "carpet outside of board";
            if (carpet.BottomRightCol - carpet.TopLeftCol == 0)
                return "the size of the carpet is 0";
            foreach(var playerOnBoard in Players)
            {
                if (carpet.Contains(playerOnBoard.Row, playerOnBoard.Col))
                    return "invalid there are players in the carpet ";
            }
            Carpet = carpet;
            FillBoardWithCarpet();
            return "";

        }

        public bool IsWithinBounds(int row, int col)
        {
            return row >= 0 && row < grid.GetLength(0) && col >= 0 && col < grid.GetLength(1);
        }

        public bool IsWithinCarpet(int row, int col)
        {
            if (Carpet == null)
            {
                throw new InvalidOperationException("carpet was not added to board");
            }
            return  Carpet.Contains(row, col);
        }

        public void MovePlayer(Player player, Direction direction)
        {
            int newRow = player.Row;
            int newCol = player.Col;

            switch (direction)
            {
                case Direction.Left:
                    newCol -= 1;
                    break;
                case Direction.Right:
                    newCol += 1;
                    break;
                case Direction.Up:
                    newRow -= 1;
                    break;
                case Direction.Down:
                    newRow += 1;
                    break;
            }
            if (ISIlegalMove(newCol, newRow))
            {
                return;
                
            }
            grid[player.Row, player.Col] = 0;
            player.Move(newCol, newRow);

            // Mark the new position
            grid[newCol, newRow] = player.Number;
        }

        private bool ISIlegalMove(int newCol,int newRow)
        {
            if (!IsWithinBounds(newCol, newRow))
            {
                Console.WriteLine("skiping turn outside of board");
                return true;
            }
            if(IsWithinPlayer(newCol, newRow))
            {
                Console.WriteLine("skiping turn there a existing pice in the new location");
                return true;
            }
            return false;
        }

        public void PrintBoard()
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            PrintFrame(rows);

            for (int i = 0; i < rows; i++)
            {
                Console.Write("# ");
                for (int j = 0; j < cols; j++)
                {
                    bool isPlayerHere = false;
                    foreach (var player in Players)
                    {
                        if (player.Row == i && player.Col == j)
                        {
                            Console.Write(player.Number.ToString() + " ");
                            isPlayerHere = true;
                            break;
                        }
                    }

                    if (!isPlayerHere && IsWithinCarpet(i, j))
                        Console.Write("* ");
                    if (!isPlayerHere && !IsWithinCarpet(i, j))
                        Console.Write(". ");
                }
                Console.WriteLine("# ");
            }
            PrintFrame(rows);
        }

        private static void PrintFrame(int rows)
        {
            for (int i = 0; i < rows + 2; i++)
            {
                Console.Write("# ");
            }
            Console.WriteLine();
        }
    }
}

