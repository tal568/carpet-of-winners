﻿using carpet_of_winners.git.Utils;

namespace carpet_of_winners.git.BL;

internal class Board
{
    private readonly int[] _gridSize;
    private readonly ConsolePrintColorString _printToScreent;
    public List<Player> Players { get; private set; }
    public Carpet? Carpet { get; private set; }

    public Board(int rows, int cols)
    {
        _gridSize = new int[2] { rows, cols };
        Players = new();
        _printToScreent = new();
    }

    public bool AddPlayer(Player player)
    {
        if (!IsWithinBounds(player.Row, player.Col))
        {
            _printToScreent.PrintColorString(
                $"Error: Player{player.Number} placed outside the board!\n",
                ConsoleColor.Red
            );
            return false;
        }
        if (IsWithinPlayer(player.Row, player.Col))
        {
            _printToScreent.PrintColorString(
                $"Error: Player{player.Number} can't move to occupied tile!\n",
                ConsoleColor.Red
            );
            return false;
        }
        Players.Add(player);
        return true;
    }

    public bool IsWithinPlayer(int row, int col)
    {
        foreach (var playerOnBoard in Players)
        {
            if (playerOnBoard.Col == col && playerOnBoard.Row == row)
                return true;
        }
        return false;
    }

    public bool AddCarpet(Carpet carpet)
    {
        if (
            !IsWithinBounds(carpet.TopLeftRow, carpet.TopLeftCol)
            || !IsWithinBounds(carpet.BottomRightRow, carpet.BottomRightCol)
        )
        {
            _printToScreent.PrintColorString(
                $"Error: Carpet dimension is outside of the board\n",
                ConsoleColor.Red
            );
            return false;
        }
        if (carpet.BottomRightCol - carpet.TopLeftCol == -1)
        {
            _printToScreent.PrintColorString(
                $"Error: The Size of the curpet must be greater then 1\n",
                ConsoleColor.Red
            );
            return false;
        }
        foreach (var playerOnBoard in Players)
        {
            if (carpet.Contains(playerOnBoard.Row, playerOnBoard.Col))
            {
                _printToScreent.PrintColorString(
                    $"Error: Can't place players on the carpet\n",
                    ConsoleColor.Red
                );
                return false;
            }
        }
        Carpet = carpet;
        return true;
    }

    public bool IsWithinBounds(int row, int col)
    {
        return row >= 0 && row < _gridSize[0] && col >= 0 && col < _gridSize[1];
    }

    public bool IsWithinCarpet(int row, int col)
    {
        return Carpet!.Contains(row, col);
    }

    public bool MovePlayer(Player player, Direction direction)
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
        if (IsIlegalMove(newRow, newCol, direction))
            return false;
        player.Move(newRow, newCol);

        PrintBoard();

        return true;
    }

    private bool IsIlegalMove(int newRow, int newCol, Direction direction)
    {
        if (direction == 0)
        {
            _printToScreent.PrintColorString(
                $"Illegal Move: player can only move by usin the keys: 1,2,3,4. skiping turn\n",
                ConsoleColor.Yellow
            );
            return true;
        }
        if (!IsWithinBounds(newRow, newCol))
        {
            _printToScreent.PrintColorString(
                $"Illegal Move: Can’t move outside the board. skiping turn\n",
                ConsoleColor.Yellow
            );
            return true;
        }
        if (IsWithinPlayer(newRow, newCol))
        {
            _printToScreent.PrintColorString(
                $"Illegal Move: Can’t Move to occupied location.  skiping turn\n",
                ConsoleColor.Yellow
            );
            return true;
        }
        return false;
    }

    public void PrintBoard()
    {
        int rows = _gridSize[0];
        int cols = _gridSize[1];
        PrintFrame(rows);

        for (int i = 0; i < rows; i++)
        {
            _printToScreent.PrintColorString("# ", ConsoleColor.Magenta);
            for (int j = 0; j < cols; j++)
            {
                bool isPlayerHere = false;
                foreach (var player in Players)
                {
                    if (player.Row == i && player.Col == j)
                    {
                        _printToScreent.PrintColorString(
                            player.Number.ToString() + " ",
                            player.Number == 1 ? ConsoleColor.Red : ConsoleColor.Green
                        );
                        isPlayerHere = true;
                        break;
                    }
                }

                if (!isPlayerHere && IsWithinCarpet(i, j))
                {
                    _printToScreent.PrintColorString("* ", ConsoleColor.Cyan);
                }
                if (!isPlayerHere && !IsWithinCarpet(i, j))
                    Console.Write(". ");
            }
            _printToScreent.PrintColorString("# \n", ConsoleColor.Magenta);
        }
        PrintFrame(rows);
    }

    private void PrintFrame(int rows)
    {
        for (int i = 0; i < rows + 2; i++)
        {
            _printToScreent.PrintColorString("# ", ConsoleColor.Magenta);
        }
        Console.WriteLine();
    }

    public int DistenseOfPlayerFromCarpet(int row, int col)
    {
        Player? player = Players.Find(player => player.Col == col && player.Row == row);
        if (player == null)
            throw new InvalidOperationException(
                $"the player  does not exist in the row:{row} col:{col}"
            );
        if (Carpet!.Contains(player.Row, player.Col))
            return 0;

        return CalcMovesNumber(player);
    }

    private int CalcMovesNumber(Player player)
    {
        int minDistensRow,
            minDistensCol;
        minDistensRow = Math.Min(
            Math.Abs(player!.Row - Carpet!.BottomRightRow),
            Math.Abs(player.Row - Carpet.TopLeftRow)
        );
        minDistensCol = Math.Min(
            Math.Abs(player.Col - Carpet.BottomRightCol),
            Math.Abs(player.Col - Carpet.TopLeftCol)
        );
        return minDistensCol + minDistensRow;
    }
}
