namespace carpet_of_winners.git;

internal class Game
{
    private Board? _board;
    private Dictionary<string, int> _gameStatistics;
    private ConsolePrintColorString _printToScreent;

    public Game()
    {
        _printToScreent = new();
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

    public void InitRound(int numberofPlayers, int rowNum, int colNum)
    {
        _board = new(rowNum, colNum);
        InitPlayers(numberofPlayers);
        InitCarpet();
        _board.PrintBoard();
    }

    public void GameLoop(int numberofPlayers, int rowNum, int colNum)
    {
        bool isGameRunning = true;
        while (isGameRunning)
        {
            InitRound(numberofPlayers, rowNum, colNum);
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
            Console.Write("do you wish to continue (y/n):");
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
        Console.WriteLine("player1 round won:" + _gameStatistics["NumberOfWinsPlayer1"]);
        Console.WriteLine("player2 round won:" + _gameStatistics["NumberOfWinsPlayer2"]);

        if (_gameStatistics["NumberOfWinsPlayer1"] > _gameStatistics["NumberOfWinsPlayer2"])
            _printToScreent.PrintColorString("player1 won the GAME\n", ConsoleColor.Green);
        else if (_gameStatistics["NumberOfWinsPlayer1"] < _gameStatistics["NumberOfWinsPlayer2"])
            _printToScreent.PrintColorString("player2 won the GAME\n", ConsoleColor.Green);
        else if (_gameStatistics["totalSteps1"] > _gameStatistics["totalSteps2"])
            _printToScreent.PrintColorString("player1 won the GAME\n", ConsoleColor.Green);
        else if (_gameStatistics["totalSteps1"] < _gameStatistics["totalSteps2"])
            _printToScreent.PrintColorString("player2 won the GAME\n", ConsoleColor.Green);
        else
            _printToScreent.PrintColorString("player2 won the GAME\n", ConsoleColor.Green);
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
                isPlayerWon = IsPlayerWon(player1.Row, player1.Col, player2.Row, player2.Col);
                if (isPlayerWon)
                {
                    Console.WriteLine($"player {player.Number} WON the round");
                    _gameStatistics["NumberOfWinsPlayer" + player.Number.ToString()] += 1;
                    break;
                }
                PrintBestChanchToWin(player1.Row, player1.Col, player2.Row, player2.Col);
            }
        }
    }

    private void InitCarpet()
    {
        bool wasCarpetAdded = false;
        while (!wasCarpetAdded)
        {
            Console.Write($"Enter carpet Top left Row:");
            int row;
            bool isIntRow = int.TryParse(Console.ReadLine(), out row);
            Console.Write($"Enter carpet Top left Col:");
            int col;
            bool isIntCol = int.TryParse(Console.ReadLine(), out col);
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
                Console.Write($"Enter user {i} Row:");
                int row;
                bool isIntRow = int.TryParse(Console.ReadLine(), out row);
                Console.Write($"Enter user {i} Col:");
                int col;
                bool isIntCol = int.TryParse(Console.ReadLine(), out col);
                if (isIntCol && isIntRow)
                    wasPlayerAdded = _board.AddPlayer(new Player(row, col, i));
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

    private void PrintBestChanchToWin(int rowIndexA, int colIndexA, int rowIndexB, int colIndexB)
    {
        int closestToWin = getWinnerWithBestChances(rowIndexA, colIndexA, rowIndexB, colIndexB);
        if (closestToWin == 0)
            return;
        if (closestToWin == 1)
            Console.WriteLine("player1 is closer to win");
        if (closestToWin == 2)
            Console.WriteLine("player2 is closer to win");
    }

    private int getWinnerWithBestChances(int rowIndexA, int colIndexA, int rowIndexB, int colIndexB)
    {
        int player1Distense = _board.DistenseOfPlayerFromCarpet(rowIndexA, colIndexA);
        int player2Distense = _board.DistenseOfPlayerFromCarpet(rowIndexB, colIndexB);
        if (player1Distense == 0 || player2Distense == 0)
            return 0;
        if (player1Distense < player2Distense)
            return 1;
        return 2;
    }
}
