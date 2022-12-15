using System.Numerics;
using System.Text.RegularExpressions;
using Advent_of_Code_2022.Lib;

namespace Advent_of_Code_2022.Days;

public partial class Day15 : DayBase<List<Day15.Sensor>, BigInteger>
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

    public override BigInteger RunPart1(List<Sensor> input)
    {
        const int y = 10;

        var ranges = GetRanges(input, y);
        var min = ranges.Min(r => r.Start);
        var max = ranges.Max(r => r.End);

        var count = 0;
        for (var x = min; x <= max; x++)
        {
            if (ranges.Any(r => r.Contains(x)) && !input.Any(i => i.Beacon.Y == y && i.Beacon.X == x))
                count++;
        }

        return count;
    }

    public override BigInteger RunPart2(List<Sensor> input)
    {
        const int searchSpace = 4000000;

        var result = new BigInteger();
        for (var y = 0; y <= searchSpace; y++)
        {
            var ranges = GetRanges(input, y,
                r => new Range<int>(Math.Max(0, r.Start), Math.Min(searchSpace, r.End)));

            var merged = true;
            var firstRange = ranges[0];
            while (merged && ranges.Count > 1)
            {
                merged = false;

                var secondRange = ranges[1];
                if (firstRange.Start <= secondRange.Start && firstRange.End >= secondRange.Start)
                {
                    firstRange.End = Math.Max(firstRange.End, secondRange.End);
                    ranges.RemoveAt(1);
                    merged = true;
                }
            }

            if (!merged || firstRange.Start != 0 || firstRange.End != searchSpace)
            {
                result = (BigInteger)(firstRange.End + 1) * searchSpace + y;
                break;
            }
        }
        
        return result;
    }

    private static List<Range<int>> GetRanges(IEnumerable<Sensor> sensors, int y,
        Func<Range<int>, Range<int>>? filter = null)
    {
        var query = sensors.Select(s => s.GetCoveredRange(y))
            .Where(r => r.HasValue)
            .Select(r => r!.Value);

        if (filter is not null)
        {
            query = query.Order()
                .Select(filter);
        }

        return query.ToList();
    }

    public readonly struct Sensor
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

        public Range<int>? GetCoveredRange(int y)
        {
            var verticalDistance = Math.Abs(y - Position.Y);
            if (verticalDistance > Distance)
                return null;

            var margin = Distance - verticalDistance;
            return new Range<int>(Position.X - margin, Position.X + margin);
        }
    }

    [GeneratedRegex(@"^Sensor at x=(?<x0>-?\d+), y=(?<y0>-?\d+): closest beacon is at x=(?<x1>-?\d+), y=(?<y1>-?\d+)$")]
    private static partial Regex ParsingRegex();
}
