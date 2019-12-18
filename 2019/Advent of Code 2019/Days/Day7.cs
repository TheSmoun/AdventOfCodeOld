using System.Linq;
using AoC2019.Extensions;
using AoC2019.Lib;
using MoreLinq;

namespace AoC2019.Days
{
    public sealed class Day7 : IntCodeDayBase<long>
    {
        public override string Name => "Day 7: Amplification Circuit";

        public override long RunPart1(long[] input)
        {
            return EnumerableEx.Range(0, 5).Permutations()
                .Max(p => p.Aggregate(0L, (current, c) => input.ToIntComputer().Run(c, current).Last()));
        }

        public override long RunPart2(long[] input)
        {
            return EnumerableEx.Range(5, 5).Permutations().Max(permutation =>
            {
                var computers = permutation.Select(item => new IntComputer(input, item)).ToQueue();

                var output = new[] {0L};
                while (computers.Count > 0)
                {
                    var computer = computers.Dequeue();
                    output = computer.Run(output).ToArray();
                    if (!computer.Halted)
                        computers.Enqueue(computer);
                }

                return output.Last();
            });
        }
    }
}
