using Advent_of_Code_2022.Extensions;

namespace Advent_of_Code_2022.Days;

public class Day06 : DayBase<string, int>
{
    public override string Name => "Day 6: Tuning Trouble";

    public override string ParseInput(IEnumerable<string> lines)
        => lines.First();

    public override int RunPart1(string input)
        => RunPart(input, 4);

    public override int RunPart2(string input)
        => RunPart(input, 14);

    private static int RunPart(string input, int differentChars)
        => Enumerable.Range(differentChars - 1, input.Length - differentChars)
            .First(i => input.Skip(i - differentChars - 1).Take(differentChars).AllDistinct()) - 1;
}
