using System;
using System.Linq;
using AoC2019.Lib;

namespace AoC2019.Days
{
    public sealed class Day11 : IntCodeAccessoryDayBase<string, PaintingRobot>
    {
        public override string Name => "Day 11: Space Police";

        public override string RunPart1(long[] input)
        {
            return RunAccessory(input, 0).PaintedPanels.ToString();
        }

        public override string RunPart2(long[] input)
        {
            var whitePanels = RunAccessory(input, 1).WhitePanels;
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
    }
}
