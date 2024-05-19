namespace carpet_of_winners.git.Utils;

public class ConsolePrintColorString
{
    private readonly ConsoleColor originalForegroundColor;

    public ConsolePrintColorString()
    {
        originalForegroundColor = Console.ForegroundColor;
    }

    public void PrintColorString(string str, ConsoleColor fgColor)
    {
        Console.ForegroundColor = fgColor;

        Console.Write(str);

        Console.ForegroundColor = originalForegroundColor;
    }
}
