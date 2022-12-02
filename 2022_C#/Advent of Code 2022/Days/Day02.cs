using Advent_of_Code_2022.Extensions;

namespace Advent_of_Code_2022.Days;

public sealed class Day02 : DayBase<IEnumerable<(int, int)>, int>
{
    public override string Name => "Day 2: Rock Paper Scissors";
    
    public override IEnumerable<(int, int)> ParseInput(IEnumerable<string> lines)
        => lines.Select(l => (l[0] - 'A' + 1, l[2] - 'X' + 1));

    public override int RunPart1(IEnumerable<(int, int)> input)
        => input.Sum(g => g.Item2 + (g.Item2 - g.Item1 + 1).Mod(3) * 3);

    public override int RunPart2(IEnumerable<(int, int)> input)
        => input.Sum(g => (g.Item1 + g.Item2).Mod(3) + 1 + (g.Item2 - 1) * 3);
}
