namespace carpet_of_winners.git.Utils;

public class ConsolePrintColorString
{
    private ConsoleColor originalForegroundColor;

    public ConsolePrintColorString()
    {
        originalForegroundColor = Console.ForegroundColor;
    }

    public void PrintColorString(string str, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(str);
        Console.ForegroundColor = originalForegroundColor;
    }
}
