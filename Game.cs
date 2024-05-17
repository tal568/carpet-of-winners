using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace carpet_of_winners.git
{
    internal class Game
    {
        private Board _board;

        public Game(int colNum,int rowNum)
        {
            _board = new(colNum,rowNum);
        }
        public void InitGame(int numberofPlayers)
        {
            InitPlayers(numberofPlayers);
            InitCarpet();
            _board.PrintBoard();
        }
        public void GameLoop()
        {  
            while (true)
            {
                foreach (var player in _board.Players) { 
                    Console.WriteLine($"user {player.Number} turn (1-up 2-down 3-left 4-right)");
                    string userMove = Console.ReadLine();
                    Direction direction;
                    Enum.TryParse(userMove,out direction);
                    _board.MovePlayer(player,direction);
                    _board.PrintBoard();
                }
            }
        }

        private void InitCarpet()
        { 
            
                string errorMessage = "_";
                while (errorMessage!="")
                {
                    Console.WriteLine($"Enter carpet Top left Col");
                    int col = int.Parse(Console.ReadLine());
                    Console.WriteLine($"Enter carpet Top left Row");
                    int row = int.Parse(Console.ReadLine());
                    Console.WriteLine($"Enter carpet size");
                    int size = int.Parse(Console.ReadLine());
                errorMessage = _board.AddCarpet(new Carpet(row, col, size));
                if(errorMessage!="")
                Console.WriteLine(errorMessage);

                }
            
        }

        private void InitPlayers(int numberofPlayers)
        {
            for (int i=1; numberofPlayers>i-1;i++)
            {
                string errorMessage = "_";
                while (errorMessage!="")
                {
                    Console.WriteLine($"Enter user {i} Col");
                    int col = int.Parse(Console.ReadLine());
                    Console.WriteLine($"Enter user {i} Row");
                    int row = int.Parse(Console.ReadLine());
                    errorMessage = _board.AddPlayer(new Player(col, row,i));
                    if (errorMessage != "")
                        Console.WriteLine(errorMessage);
                }
            }
        }
    }
}
