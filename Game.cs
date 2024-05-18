using carpet_of_winners.git.Utils;
using System.Numerics;

namespace carpet_of_winners.git;

internal class Game
{
    private Board? _board;
    private readonly Dictionary<string, int> _gameStatistics;
    private readonly ConsolePrintColorString _printToScreent;

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
            _board = new(30, 30);
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
            PrintVictoryMessgae(1, true);
        else if (_gameStatistics["NumberOfWinsPlayer1"] < _gameStatistics["NumberOfWinsPlayer2"])
            PrintVictoryMessgae(2, true);


        else if (_gameStatistics["totalSteps1"] > _gameStatistics["totalSteps2"])
            PrintVictoryMessgae(1, true);
        else if (_gameStatistics["totalSteps1"] < _gameStatistics["totalSteps2"])
            _printToScreent.PrintColorString("player2 won the GAME\n", ConsoleColor.Green);
        else
            PrintVictoryMessgae(2, true);

    }

    private void Round()
    {
        Player player1 = _board!.Players.First();
        Player player2 = _board!.Players.ElementAt(1);
        bool isPlayerWon = false;
        while (!isPlayerWon)
        {
            foreach (var player in _board.Players)
            {
                Direction direction;
                Char userMove= GetMoveDiractionFromUserInput(player.Number);
                Enum.TryParse(userMove.ToString(), out direction);
                if (_board.MovePlayer(player, direction))
                    _gameStatistics["totalSteps" + player.Number.ToString()] += 1;
                isPlayerWon = IsPlayerWon(player1.Row, player1.Col, player2.Row, player2.Col);
                if (isPlayerWon)
                {
                    PrintVictoryMessgae(player.Number,false);
                    _gameStatistics["NumberOfWinsPlayer" + player.Number.ToString()] += 1;
                    break;
                }
                PrintBestChanchToWin(player1.Row, player1.Col, player2.Row, player2.Col);
            }
        }

      
    }
    private void PrintVictoryMessgae(int playerNumber,bool isFinalGame)
    {
        string stage = (isFinalGame ? "GAME!!!" : "Round");
            _printToScreent.PrintColorString("* * * * * * * * * * * * * * * * * * * *\n", ConsoleColor.Cyan);
            _printToScreent.PrintColorString($"player {playerNumber} is the Winner of the {stage}\n", ConsoleColor.Cyan);
            _printToScreent.PrintColorString("* * * * * * * * * * * * * * * * * * * *\n", ConsoleColor.Cyan);
    }
    private Char GetMoveDiractionFromUserInput(int playerNumber)
    {
        Console.Write($"user {playerNumber} turn (1-up 2-down 3-left 4-right):");
        Char userMove = Console.ReadKey().KeyChar;
        Console.WriteLine("");
        return userMove;
    }

    private void InitCarpet()
    {
        bool wasCarpetAdded = false;
        while (!wasCarpetAdded)
        {
            Carpet? carpet = CreateCarpetFromUserInput();
            if (carpet is not null)
                wasCarpetAdded = _board!.AddCarpet(carpet);
            else
                Console.WriteLine("only int values alowed");
        }
    }
    private  Carpet? CreateCarpetFromUserInput()
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
        if (!isIntCol || !isIntRow || !isIntSize)
            return null;
        return new(row, col, size);
    }
    

    

    private void InitPlayers(int numberofPlayers)
    {
        for (int i = 1; numberofPlayers > i - 1; i++)
        {
            bool wasPlayerAdded = false;
            while (!wasPlayerAdded)
            {

                Player? player = CreatePlayerFromUserInput(i);
                if(player is not null)
                    wasPlayerAdded = _board!.AddPlayer(player);
                else
                    Console.WriteLine("only int values alowed");
            }
        }
    }
   private Player? CreatePlayerFromUserInput(int playerNumber)
    {
        Console.Write($"Enter user {playerNumber} Row:");
        int row;
        bool isIntRow = int.TryParse(Console.ReadLine(), out row);
        Console.Write($"Enter user {playerNumber} Col:");
        int col;
        bool isIntCol = int.TryParse(Console.ReadLine(), out col);
        if (!isIntCol || !isIntRow)
            return null;
        return new(row,col,playerNumber);
    }
    private bool IsPlayerWon(int rowIndexA, int colIndexA, int rowIndexB, int colIndexB)
    {
        if (_board!.Carpet!.Contains(rowIndexA, colIndexA))
            return true;
        if (_board!.Carpet.Contains(rowIndexB, colIndexB))
            return true;
        return false;
    }

    private void PrintBestChanchToWin(int rowIndexA, int colIndexA, int rowIndexB, int colIndexB)
    {
        int closestToWin = getWinnerWithBestChances(rowIndexA, colIndexA, rowIndexB, colIndexB);
        switch (closestToWin)
        {
            case (0):
                _printToScreent.PrintColorString("both player are the same distense\n", ConsoleColor.Cyan);
                break;
            case (1):
                _printToScreent.PrintColorString("player1 is closer to win\n", ConsoleColor.Cyan);
                break;

            case (2):
                _printToScreent.PrintColorString("player2 is closer to win\n", ConsoleColor.Cyan);
                break;
        }
    }

    private int getWinnerWithBestChances(int rowIndexA, int colIndexA, int rowIndexB, int colIndexB)
    {
        int player1Distense = _board!.DistenseOfPlayerFromCarpet(rowIndexA, colIndexA);
        int player2Distense = _board.DistenseOfPlayerFromCarpet(rowIndexB, colIndexB);

        if (player1Distense < player2Distense)
            return 1;
        if (player1Distense > player2Distense)
            return 2;
        return 0;
    }
}
