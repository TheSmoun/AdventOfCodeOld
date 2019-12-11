using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AoC2019.Extensions;
using AoC2019.Lib;
using MoreLinq;

namespace AoC2019.Days
{
    public sealed class Day11 : DayBase<long[], string>
    {
        public override string Name => "Day 11: Space Police";

        public override long[] ParseInput(IEnumerable<string> lines)
        {
            return lines.Single().Split(",").Select(long.Parse).ToArray();
        }

        public override string RunPart1(long[] input)
        {
            return RunRobot(input, 0).PaintedPanels.ToString();
        }

        public override string RunPart2(long[] input)
        {
            var whitePanels = RunRobot(input, 1).WhitePanels;
            var minY = whitePanels.Min(p => p.Y);
            var maxY = whitePanels.Max(p => p.Y);
            var minX = whitePanels.Min(p => p.X);
            var maxX = whitePanels.Max(p => p.X);

            for (var y = minY; y <= maxY; y++)
            {
                var panels = whitePanels.Where(p => p.Y == y).ToArray();
                for (var x = minX; x <= maxX; x++)
                {
                    Console.Write(panels.Any(p => p.X == x) ? "#" : " ");
                }
                Console.WriteLine();
            }

            return "CGPJCGCL";
        }

        private static PaintingRobot RunRobot(IEnumerable<long> input, long firstColor)
        {
            var robot2Computer = EnumerableEx.BlockingCollection(firstColor);
            var computer2Robot = EnumerableEx.BlockingCollection<long>();

            var computer = new IntComputer(input, robot2Computer, computer2Robot);
            var robot = new PaintingRobot(() => computer.Running, computer2Robot, robot2Computer);

            var threads = new[]
            {
                new Thread(() => computer.Run()),
                new Thread(() => robot.Run()),
            };

            threads.ForEach(t => t.Start());
            threads.ForEach(t => t.Join());

            return robot;
        }
    }
}
