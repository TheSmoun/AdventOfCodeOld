namespace Advent_of_Code_2022.Extensions;

public static class ConsoleEx
{
    public static void WriteColoredLine(string line, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(line);
        Console.ResetColor();
    }
}
