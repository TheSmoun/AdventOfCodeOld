using MoreLinq;

namespace Advent_of_Code_2022.Days;

public sealed class Day01 : DayBase<IEnumerable<int>, int>
{
    public override string Name => "Day 1: Calorie Counting";

    public override IEnumerable<int> ParseInput(IEnumerable<string> lines)
        => lines.Select(l => string.IsNullOrEmpty(l) ? -1 : int.Parse(l))
            .Split(-1)
            .Select(c => c.Sum())
            .OrderDescending();

    public override int RunPart1(IEnumerable<int> input)
        => input.First();

    public override int RunPart2(IEnumerable<int> input)
        => input.Take(3).Sum();
}
