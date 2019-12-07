using System.Collections.Generic;
using System.Linq;
using AoC2019.Lib;

namespace AoC2019.Days
{
    public sealed class Day5 : DayBase<int[], int>
    {
        protected override string Name => "Day 5: Sunny with a Chance of Asteroids";

        protected override int[] ParseInput(IEnumerable<string> lines)
        {
            return lines.Single().Split(",").Select(int.Parse).ToArray();
        }

        protected override int RunPart1(int[] input)
        {
            return new IntComputer(input, new Queue<int>(new[] {1})).Run();
        }

        protected override int RunPart2(int[] input)
        {
            return new IntComputer(input, new Queue<int>(new[] {5})).Run();
        }
    }
}
