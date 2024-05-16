﻿using System;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[,] board = InitBoard();
              PrintBoard(board);

            SetInitPlayerPosionOnBoard(board);
            PrintBoard(board);


        }
        public static string[,] InitBoard()
        {
            string[,] board = new string[32, 32];
            for (int col=0; col<board.GetLength(0); col++)
                for(int row=0; row<board.GetLength(1); row++)
                {
                    if(col==0 || row == 0||col==31||row==31)
                    {
                        board[col, row] = "#";
                    }
                    else
                        board[col, row] = " ";

                }
            return board;
        }
        public static void PrintBoard(string[,] board)
        {
            for (int col = 0; col < board.GetLength(0); col++)
            {
                for (int row = 0; row < board.GetLength(1); row++)
                {
                    Console.Write(board[col, row]);
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
        }
        public static void SetInitPlayerPosionOnBoard(string[,] board)
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
                    if (board[colIndex,rowIndex]!=" ")
                    {
                        isIlegalMove=false;
                    }
                    else
                    {
                        Console.WriteLine($"Player {i} is in ilegal posison");
                    }

                }
                board[colIndex, rowIndex] = (i + 1).ToString();
            }
        }

    }
}