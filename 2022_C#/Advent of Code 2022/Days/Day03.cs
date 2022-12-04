using System.Diagnostics;

namespace Advent_of_Code_2022.Days;

public class Day03 : DayBase<IEnumerable<(string, string)>, int>
{
    public override string Name => "Day 3: Rucksack Reorganization";

    public override IEnumerable<(string, string)> ParseInput(IEnumerable<string> lines)
        => lines.Select(l => (l[..(l.Length / 2)], l[(l.Length / 2)..]));

    public override int RunPart1(IEnumerable<(string, string)> input)
        => input.Select(r => r.Item1.Intersect(r.Item2).First())
            .Sum(i => i switch
            {
                >= 'A' and <= 'Z' => i - 'A' + 27,
                >= 'a' and <= 'z' => i - 'a' + 1,
                _ => throw new UnreachableException()
            });

    public override int RunPart2(IEnumerable<(string, string)> input)
    {
        // throw new NotImplementedException();
        return default;
    }
}
