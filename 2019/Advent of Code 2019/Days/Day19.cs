using System.Linq;
using AoC2019.Lib;

namespace AoC2019.Days
{
    public sealed class Day19 : IntCodeDayBase<long>
    {
        public override string Name => "Day 19: Tractor Beam";

        public override long RunPart1(long[] input)
        {
            var sum = 0L;
            for (var y = 0; y < 50; y++)
            {
                for (var x = 0; x < 50; x++)
                {
                    sum += new IntComputer(input, x, y).Run().Single();
                }
            }

            return sum;
        }

        public override long RunPart2(long[] input)
        {
            bool InBeam(long x, long y) => new IntComputer(input, x, y).Run().Single() == 1L;

            var x = 10;
            var y = 0;

            while (true)
            {
                if (InBeam(x, y))
                {
                    if (InBeam(x - 99, y + 99))
                        return (x - 99) * 10000 + y;

                    x++;
                }
                else
                {
                    y++;
                }
            }
        }
    }
}
