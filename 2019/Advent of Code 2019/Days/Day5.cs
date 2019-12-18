using System.Linq;
using AoC2019.Extensions;

namespace AoC2019.Days
{
    public sealed class Day5 : IntCodeDayBase<long>
    {
        public override string Name => "Day 5: Sunny with a Chance of Asteroids";

        public override long RunPart1(long[] input)
        {
            return input.ToIntComputer().Run(1).Last();
        }

        public override long RunPart2(long[] input)
        {
            return input.ToIntComputer().Run(5).Last();
        }
    }
}
