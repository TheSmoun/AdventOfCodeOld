using System.Collections.Generic;
using System.Linq;
using AoC2019.Extensions;

namespace AoC2019.Days
{
    public sealed class Day9 : DayBase<long[], long>
    {
        public override string Name => "Day 9: Sensor Boost";

        public override long[] ParseInput(IEnumerable<string> lines)
        {
            return lines.Single().Split(",").Select(long.Parse).ToArray();
        }

        public override long RunPart1(long[] input)
        {
            return input.ToIntComputer(1).Run();
        }

        public override long RunPart2(long[] input)
        {
            return input.ToIntComputer(2).Run();
        }
    }
}
