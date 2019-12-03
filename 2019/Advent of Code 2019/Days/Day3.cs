using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2019.Days
{
    public sealed class Day3 : DayBase<Dictionary<(int, int), int>[], int>
    {
        protected override string Name => "Day 3: Crossed Wires";

        protected override Dictionary<(int, int), int>[] ParseInput(IEnumerable<string> lines)
        {
            var cables = lines.Select(l => l.Split(",").Select(i => (i[0], int.Parse(i.Substring(1)))));
            var list = new List<Dictionary<(int, int), int>>();

            var deltas = new Dictionary<char, (int, int)>
            {
                {'R', (-1, 0)},
                {'L', (1, 0)},
                {'U', (0, 1)},
                {'D', (0, -1)}
            };

            foreach (var cable in cables)
            {
                var positions = new Dictionary<(int, int), int>();
                var pos = (0, 0);
                var distance = 0;
                foreach (var (dir, dist) in cable)
                {
                    var (dx, dy) = deltas[dir];
                    for (var i = 0; i < dist; i++)
                    {
                        pos = (pos.Item1 + dx, pos.Item2 + dy);
                        positions.TryAdd(pos, ++distance);
                    }
                }
                list.Add(positions);
            }

            return list.ToArray();
        }

        protected override int RunPart1(Dictionary<(int, int), int>[] input)
        {
            return input[0].Keys.Intersect(input[1].Keys).Min(p => Math.Abs(p.Item1) + Math.Abs(p.Item2));
        }

        protected override int RunPart2(Dictionary<(int, int), int>[] input)
        {
            return input[0].Keys.Intersect(input[1].Keys).Min(p => input[0][p] + input[1][p]);
        }
    }
}
