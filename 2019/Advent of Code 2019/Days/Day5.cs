using System.Collections.Generic;
using System.Linq;
using AoC2019.Extensions;

namespace AoC2019.Days
{
    public sealed class Day5 : DayBase<int[], int>
    {
        public override string Name => "Day 5: Sunny with a Chance of Asteroids";

        public override int[] ParseInput(IEnumerable<string> lines)
        {
            return lines.Single().Split(",").Select(int.Parse).ToArray();
        }

        public override int RunPart1(int[] input)
        {
            return input.ToIntComputer(1).Run();
        }

        public override int RunPart2(int[] input)
        {
            return input.ToIntComputer(5).Run();
        }
    }
}
