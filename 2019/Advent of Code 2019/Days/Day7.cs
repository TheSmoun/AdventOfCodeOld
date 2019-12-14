using System.Linq;
using System.Threading;
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
                .Max(p => p.Aggregate(0L, (current, c) => input.ToIntComputer(c, current).Run()));
        }

        public override long RunPart2(long[] input)
        {
            return EnumerableEx.Range(5, 5).Permutations().Max(permutation =>
            {
                var eToA = EnumerableEx.BlockingCollection(permutation[0], 0);
                var aToB = EnumerableEx.BlockingCollection(permutation[1]);
                var bToC = EnumerableEx.BlockingCollection(permutation[2]);
                var cToD = EnumerableEx.BlockingCollection(permutation[3]);
                var dToE = EnumerableEx.BlockingCollection(permutation[4]);

                var ampA = new IntComputer(input, eToA, aToB);
                var ampB = new IntComputer(input, aToB, bToC);
                var ampC = new IntComputer(input, bToC, cToD);
                var ampD = new IntComputer(input, cToD, dToE);
                var ampE = new IntComputer(input, dToE, eToA);

                var threads = new[]
                {
                    new Thread(() => ampA.Run()),
                    new Thread(() => ampB.Run()),
                    new Thread(() => ampC.Run()),
                    new Thread(() => ampD.Run()),
                    new Thread(() => ampE.Run())
                };

                threads.ForEach(t => t.Start());
                threads.ForEach(t => t.Join());

                return ampE.LastOutput;
            });
        }
    }
}
