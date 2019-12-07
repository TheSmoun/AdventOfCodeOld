using System.Collections.Generic;
using System.Linq;
using AoC2019.Extensions;

namespace AoC2019.Days
{
    public sealed class Day1 : DayBase<IEnumerable<int>, int>
    {
        public override string Name => "Day 1: The Tyranny of the Rocket Equation";

        public override IEnumerable<int> ParseInput(IEnumerable<string> lines)
        {
            return lines.Select(int.Parse);
        }

        public override int RunPart1(IEnumerable<int> input)
        {
            return input.Sum(Fuel);
        }

        public override int RunPart2(IEnumerable<int> input)
        {
            return input.Sum(m => Fuel(m).Sequence(Fuel).TakeWhile(f => f > 0).Sum());
        }

        private static int Fuel(int moduleMass) => moduleMass / 3 - 2;
    }
}
