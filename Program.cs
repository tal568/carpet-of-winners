using System;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char[,] board = InitBoard();
              PrintBoard(board);

            SetInitPlayerPosionOnBoard(board);
            PrintBoard(board);


        }
        public static char[,] InitBoard()
        {
            char[,] board = new char[30, 30];
            for (int col=0; col<board.GetLength(0); col++)
                for(int row=0; row<board.GetLength(1); row++)
                {
                        board[col, row] = ' ';

                }
            return board;
        }
        public static void PrintBoard(char[,] board)
        {
            for (int col = 0; col < board.GetLength(0); col++)
            {
                for (int row = 0; row < board.GetLength(1); row++)
                {
                    Console.Write(board[col, row]+" ");
                }
                Console.WriteLine();
            }
        }
        public static void SetInitPlayerPosionOnBoard(char[,] board)
        {
            int NUMBER_OF_PLAYERS = 2;
            for (int i = 1; i < NUMBER_OF_PLAYERS+1; i++)
            {   
                int rowIndex=0, colIndex=0;
                bool isIlegalMove = true;
               
                while (isIlegalMove)
                {
                    Console.WriteLine($"enter user {i} row index");
                     rowIndex = int.Parse(Console.ReadLine());
                    Console.WriteLine($"enter user {i} col index");
                     colIndex = int.Parse(Console.ReadLine());
                    if (board[colIndex,rowIndex]!=' ')
                    {
                        isIlegalMove=false;
                    }
                    else
                    {
                        Console.WriteLine($"Player {i} is in ilegal posison");
                    }

                }
                board[colIndex, rowIndex] = (char)(i + 1);
            }
        }

    }
}