using System.Linq;
using AoC2019.Extensions;

namespace AoC2019.Days
{
    public sealed class Day9 : IntCodeDayBase<long>
    {
        public override string Name => "Day 9: Sensor Boost";

        public override long RunPart1(long[] input)
        {
            return input.ToIntComputer().Run(1).Last();
        }

        public override long RunPart2(long[] input)
        {
            return input.ToIntComputer().Run(2).Last();
        }
    }
}
