using System.Text.RegularExpressions;
using Advent_of_Code_2022.Extensions;

namespace Advent_of_Code_2022.Days;

public class Day04 : DayBase<IEnumerable<(Range, Range)>, int>
{
    private const string Pattern = @"^(\d+)\-(\d+),(\d+)\-(\d+)$";
    private static readonly Regex Regex = new(Pattern);
    
    public override string Name => "Day 4: Camp Cleanup";

    public override IEnumerable<(Range, Range)> ParseInput(IEnumerable<string> lines)
        => lines.Select(l =>
        {
            var match = Regex.Match(l);
            return (new Range(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value) + 1),
                new Range(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value) + 1));
        });

    public override int RunPart1(IEnumerable<(Range, Range)> input)
        => input.Count(i => i.Item1.Contains(i.Item2) || i.Item2.Contains(i.Item1));

    public override int RunPart2(IEnumerable<(Range, Range)> input)
        => input.Count(i => i.Item1.Overlaps(i.Item2) || i.Item2.Overlaps(i.Item1));
}
