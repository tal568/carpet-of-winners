using carpet_of_winners.git;

Board board = new Board(30, 30);

// Create players
Player A = new Player(0, 8, 2);
Player B = new Player(1, 20, 1);

// Add players to the board
board.AddCarpet(new(20, 20, 5));
Console.WriteLine("is valid ? :"+board.AddPlayer(A));
Console.WriteLine("is valid ? :" + board.AddPlayer(B));

// Print the initial board state
Console.WriteLine("Initial board state:");
board.PrintBoard();

// Move players
board.MovePlayer(A, Direction.Right);
board.MovePlayer(B, Direction.Up);

// Print the board state after moving players
Console.WriteLine("\nBoard state after moving players:");
board.PrintBoard();

Game game = new();
game.InitGame(2);
game.GameLoop();