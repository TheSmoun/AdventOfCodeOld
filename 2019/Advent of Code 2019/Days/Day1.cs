using System;
using System.Collections.Generic;
using System.Linq;
using AoC2019.Extensions;

namespace AoC2019.Days
{
    public sealed class Day1 : DayBase<IEnumerable<int>, int>
    {
        protected override string Name => "Day 1: The Tyranny of the Rocket Equation";

        protected override IEnumerable<int> ParseInput(IEnumerable<string> lines)
        {
            return lines.Select(int.Parse);
        }

        protected override int RunPart1(IEnumerable<int> input)
        {
            return input.Sum(Fuel);
        }

        protected override int RunPart2(IEnumerable<int> input)
        {
            return input.Sum(m => Fuel(m).Sequence(Fuel).TakeWhile(f => f > 0).Sum());
        }

        private static int Fuel(int moduleMass) => moduleMass / 3 - 2;
    }
}
