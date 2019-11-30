using System;
using System.Collections.Generic;
using System.Linq;
using AoC2019.Extensions;

namespace AoC2019.Days
{
    public sealed class Day1 : DayBase<IEnumerable<int>>
    {
        protected override string Name => "Day 1";

        protected override IEnumerable<int> ParseInput(IEnumerable<string> lines)
        {
            return lines.Select(l => Convert.ToInt32(l));
        }

        protected override string RunPart1(IEnumerable<int> input)
        {
            return input.Sum().ToString();
        }

        protected override string RunPart2(IEnumerable<int> input)
        {
            var known = new HashSet<int>();

            var frequency = 0;
            foreach (var change in input.Cycle())
            {
                known.Add(frequency);
                frequency += change;
                if (known.Contains(frequency))
                    return $"{frequency}";
            }

            throw new InvalidOperationException();
        }
    }
}
