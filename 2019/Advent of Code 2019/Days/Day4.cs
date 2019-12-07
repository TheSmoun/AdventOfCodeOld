using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace AoC2019.Days
{
    public sealed class Day4 : DayBase<(int, int), int>
    {
        public override string Name => "Day 4: Secure Container";

        public override (int, int) ParseInput(IEnumerable<string> lines)
        {
            var numbers = lines.Single().Split("-").Select(int.Parse).ToArray();
            return (numbers[0], numbers[1]);
        }

        public override int RunPart1((int, int) input)
        {
            return GetCandidates(input).Count(s => s.GroupAdjacent(c => c).Any(c => c.Count() >= 2));
        }

        public override int RunPart2((int, int) input)
        {
            return GetCandidates(input).Count(s => s.GroupAdjacent(c => c).Any(c => c.Count() == 2));
        }

        private static IEnumerable<string> GetCandidates((int, int) input)
        {
            var (start, end) = input;
            return Enumerable.Range(start, end - start)
                .Select(i => i.ToString())
                .Where(s => s.Window(2).All(p => p[0] <= p[1]));
        }
    }
}
