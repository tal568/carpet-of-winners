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

        public Game()
        {
            _board = new(30,30);
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
                    Console.WriteLine($"user {player.Number} turn (1-up 2-down 3-left 4-down)");
                    string userMove = Console.ReadLine();
                    Direction direction;
                    Enum.TryParse<Direction>(userMove,out direction);
                    _board.MovePlayer(player,direction);
                    _board.PrintBoard();
                }
            }
        }

        private void InitCarpet()
        { 
            
                bool isAdded = false;
                while (!isAdded)
                {
                    Console.WriteLine($"Enter carpet Top left Col");
                    int col = int.Parse(Console.ReadLine());
                    Console.WriteLine($"Enter carpet Top left Row");
                    int row = int.Parse(Console.ReadLine());
                    Console.WriteLine($"Enter carpet size");
                    int size = int.Parse(Console.ReadLine());
                    isAdded = _board.AddCarpet(new Carpet(row, col, size - 1));
                    if (!isAdded)
                    {
                        Console.WriteLine("invalid posison for carpet");
                    }

                }
            
        }

        private void InitPlayers(int numberofPlayers)
        {
            for (int i=1; numberofPlayers>i-1;i++)
            {
                bool isAdded = false;
                while (!isAdded)
                {
                    Console.WriteLine($"Enter user {i} Col");
                    int col = int.Parse(Console.ReadLine());
                    Console.WriteLine($"Enter user {i} Row");
                    int row = int.Parse(Console.ReadLine());
                    isAdded = _board.AddPlayer(new Player(col, row,i));
                    if (!isAdded)
                    {
                        Console.WriteLine($"invalid posison for player {i}");
                    }
                }
            }
        }
    }
}
