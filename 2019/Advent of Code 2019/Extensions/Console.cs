using System;

namespace AoC2019.Extensions
{
    public static class ConsoleEx
    {
        public static void WriteColoredLine(string line, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(line);
            Console.ResetColor();
        }

        public static void WriteColored(string s, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(s);
            Console.ResetColor();
        }
    }
}
