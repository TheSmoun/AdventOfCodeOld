using System.Diagnostics;

namespace Advent_of_Code_2022.Days;

public class Day03 : DayBase<IEnumerable<string>, int>
{
    public override string Name => "Day 3: Rucksack Reorganization";

    public override IEnumerable<string> ParseInput(IEnumerable<string> lines) => lines;

    public override int RunPart1(IEnumerable<string> input)
        => input.Select(l => (l[..(l.Length / 2)], l[(l.Length / 2)..]))
            .Select(r => r.Item1.Intersect(r.Item2).First())
            .Sum(GetItemPriority);

    public override int RunPart2(IEnumerable<string> input)
        => input.Chunk(3)
            .Select(g => g[0].Intersect(g[1]).Intersect(g[2]).First())
            .Sum(GetItemPriority);

    private static int GetItemPriority(char item) => item switch
    {
        >= 'A' and <= 'Z' => item - 'A' + 27,
        >= 'a' and <= 'z' => item - 'a' + 1,
        _ => throw new UnreachableException()
    };
}
