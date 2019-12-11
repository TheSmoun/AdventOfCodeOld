using System;
using System.Collections.Generic;
using System.Linq;
using AoC2019.Extensions;
using MoreLinq;

namespace AoC2019.Days
{
    public sealed class Day10 : DayBase<List<(int X, int Y)>, int>
    {
        public override string Name => "Day 10: Monitoring Station";

        public override List<(int X, int Y)> ParseInput(IEnumerable<string> lines)
        {
            var points = new List<(int X, int Y)>();
            var y = 0;
            foreach (var line in lines)
            {
                var x = 0;
                foreach (var c in line.ToCharArray())
                {
                    if (c == '#')
                    {
                        points.Add((x, y));
                    }

                    x++;
                }

                y++;
            }

            return points;
        }

        public override int RunPart1(List<(int X, int Y)> input)
        {
            return input.Max(a => CalcVisible(input, a));
        }

        public override int RunPart2(List<(int X, int Y)> input)
        {
            var station = input.MaxBy(a => CalcVisible(input, a)).First();
            return input.Where(a => a != station).Select(a =>
            {
                var xDist = a.X - station.X;
                var yDist = a.Y - station.Y;
                var angle = Math.Atan2(xDist, yDist);
                return (a.X, a.Y, Angle: angle, Dist: Math.Sqrt(xDist * xDist + yDist * yDist));
            }).ToLookup(a => a.Angle).OrderByDescending(a => a.Key).Select(a => a.OrderBy(b => b.Dist).ToQueue())
            .Repeat().SelectMany(EnumerableEx.DequeueOnce).Skip(199).Take(1).Select(a => a.X * 100 + a.Y).Single();
        }

        private static int CalcVisible(IEnumerable<(int X, int Y)> input, (int X, int Y) a)
        {
            return input.Where(b => a != b).Select(b => Math.Atan2(a.Y - b.Y, a.X - b.X)).Distinct().Count();
        }
    }
}
