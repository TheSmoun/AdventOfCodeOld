using System.Text.RegularExpressions;
using Advent_of_Code_2022.Lib;

namespace Advent_of_Code_2022.Days;

public partial class Day15 : DayBase<List<Day15.Sensor>, int>
{
    private static readonly Regex Regex = ParsingRegex();
    
    public override string Name => "Day 15: Beacon Exclusion Zone";
    
    public override List<Sensor> ParseInput(IEnumerable<string> lines)
    {
        return lines.Select(l =>
        {
            var match = Regex.Match(l);
            var x0 = int.Parse(match.Groups["x0"].Value);
            var y0 = int.Parse(match.Groups["y0"].Value);
            var x1 = int.Parse(match.Groups["x1"].Value);
            var y1 = int.Parse(match.Groups["y1"].Value);
            return new Sensor(new Vec2<int>(x0, y0), new Vec2<int>(x1, y1));
        }).ToList();
    }

    public override int RunPart1(List<Sensor> input)
    {
        var coveredRanges = input.Select(s => s.GetCoveredRange(10))
            .Where(r => r.HasValue)
            .Select(r => r!.Value)
            .ToArray();
        
        throw new NotImplementedException();
    }

    public override int RunPart2(List<Sensor> input)
    {
        throw new NotImplementedException();
    }

    public struct Sensor
    {
        public Vec2<int> Position { get; }
        public Vec2<int> Beacon { get; }
        public int Distance { get; }

        public Sensor(Vec2<int> position, Vec2<int> beacon)
        {
            Position = position;
            Beacon = beacon;
            var diff = Beacon - Position;
            Distance = Math.Abs(diff.X) + Math.Abs(diff.Y);
        }

        public IntRange? GetCoveredRange(int y)
        {
            var verticalDistance = Math.Abs(y - Position.Y);
            if (verticalDistance > Distance)
                return null;

            var margin = Distance - verticalDistance;
            return new IntRange(Position.X - margin, Position.X + margin);
        }
    }

    [GeneratedRegex(@"^Sensor at x=(?<x0>-?\d+), y=(?<y0>-?\d+): closest beacon is at x=(?<x1>-?\d+), y=(?<y1>-?\d+)$")]
    private static partial Regex ParsingRegex();
}
