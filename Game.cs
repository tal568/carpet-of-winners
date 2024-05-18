namespace carpet_of_winners.git;

internal class Game
{
    private Board? _board;
    private Dictionary<string, int> _gameStatistics;

    public Game()
    {
        _board = null;
        _gameStatistics = new Dictionary<string, int>
        {
            { "totalSteps1", 0 },
            { "totalSteps2", 0 },
            { "NumberOfWinsPlayer1", 0 },
            { "NumberOfWinsPlayer2", 0 }
        };
        ;
    }

    public void InitRound(int numberofPlayers, int colNum, int rowNum)
    {
        _board = new(colNum, rowNum);
        InitPlayers(numberofPlayers);
        InitCarpet();
        _board.PrintBoard();
    }

    public void GameLoop(int numberofPlayers, int colNum, int rowNum)
    {
        bool isGameRunning = true;
        while (isGameRunning)
        {
            InitRound(numberofPlayers, colNum, rowNum);
            Round();
            isGameRunning = CheakIfUserWhantToContinue();
        }
        PrintGameStatistics();
    }

    private bool CheakIfUserWhantToContinue()
    {
        bool isInpuValid = false;
        while (!isInpuValid)
        {
            Console.WriteLine("do you wish to continue (y/n)");
            Char userResponse = Console.ReadKey().KeyChar;
            Console.WriteLine("");
            switch (userResponse)
            {
                case 'n':
                    return false;
                case 'y':
                    return true;
            }
        }
        return true;
    }

    public void PrintGameStatistics()
    {
        Console.WriteLine("player1 wins:" + _gameStatistics["NumberOfWinsPlayer1"]);
        Console.WriteLine("player2 wins:" + _gameStatistics["NumberOfWinsPlayer2"]);

        if (_gameStatistics["NumberOfWinsPlayer1"] > _gameStatistics["NumberOfWinsPlayer2"])
            Console.WriteLine("player1 won");
        else if (
            _gameStatistics["NumberOfWinsPlayer1"] < _gameStatistics["NumberOfWinsPlayer2"]
        )
            Console.WriteLine("player2 won");
        else if (_gameStatistics["totalSteps1"] > _gameStatistics["totalSteps2"])
            Console.WriteLine("player1 won");
        else if (_gameStatistics["totalSteps1"] < _gameStatistics["totalSteps2"])
            Console.WriteLine("player2 won");
        else
            Console.WriteLine("player2 won");
    }

    private void Round()
    {
        Player player1 = _board.Players.First();
        Player player2 = _board.Players.ElementAt(1);
        bool isPlayerWon = false;
        while (!isPlayerWon)
        {
            foreach (var player in _board.Players)
            {
                Console.Write($"user {player.Number} turn (1-up 2-down 3-left 4-right):");
                Char userMove = Console.ReadKey().KeyChar;
                Console.WriteLine("");
                Direction direction;
                Enum.TryParse(userMove.ToString(), out direction);
                if (_board.MovePlayer(player, direction))
                    _gameStatistics["totalSteps" + player.Number.ToString()] += 1;
                _board.PrintBoard();
                isPlayerWon = IsPlayerWon(player1.Col, player1.Row, player2.Col, player2.Row);
                if (isPlayerWon)
                {
                    Console.WriteLine($"player {player.Number} WON");
                    _gameStatistics["NumberOfWinsPlayer" + player.Number.ToString()] += 1;
                    break;
                }
            }
        }
    }

    private void InitCarpet()
    {
        bool wasCarpetAdded = false;
        while (!wasCarpetAdded)
        {
            Console.Write($"Enter carpet Top left Col:");
            int col;
            bool isIntCol = int.TryParse(Console.ReadLine(), out col);
            Console.Write($"Enter carpet Top left Row:");
            int row;
            bool isIntRow = int.TryParse(Console.ReadLine(), out row);
            Console.Write($"Enter carpet size:");
            int size;
            bool isIntSize = int.TryParse(Console.ReadLine(), out size);
            if (isIntCol && isIntRow && isIntSize)
                wasCarpetAdded = _board.AddCarpet(new Carpet(row, col, size));
            else
                Console.WriteLine("only int values alowed");
        }
    }

    private void InitPlayers(int numberofPlayers)
    {
        for (int i = 1; numberofPlayers > i - 1; i++)
        {
            bool wasPlayerAdded = false;
            while (!wasPlayerAdded)
            {
                Console.Write($"Enter user {i} Col:");
                int col;
                bool isIntCol = int.TryParse(Console.ReadLine(), out col);
                Console.Write($"Enter user {i} Row:");
                int row;
                bool isIntRow = int.TryParse(Console.ReadLine(), out row);
                if (isIntCol && isIntRow)
                    wasPlayerAdded = _board.AddPlayer(new Player(col, row, i));
                else
                    Console.WriteLine("only int values alowed");
            }
        }
    }

    private bool IsPlayerWon(int rowIndexA, int colIndexA, int rowIndexB, int colIndexB)
    {
        if (_board.Carpet.Contains(rowIndexA, colIndexA))
            return true;
        if (_board.Carpet.Contains(rowIndexB, colIndexB))
            return true;
        return false;
    }
}
