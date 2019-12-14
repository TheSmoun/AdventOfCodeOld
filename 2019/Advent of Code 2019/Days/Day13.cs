using AoC2019.Lib;

namespace AoC2019.Days
{
    public sealed class Day13 : IntCodeAccessoryDayBase<long, ArcadeCabinet>
    {
        public override string Name => "Day 13: Care Package";

        public override long RunPart1(long[] input)
        {
            return RunAccessory(input).Blocks;
        }

        public override long RunPart2(long[] input)
        {
            input[0] = 2;
            return RunAccessory(input).Score;
        }
    }
}
