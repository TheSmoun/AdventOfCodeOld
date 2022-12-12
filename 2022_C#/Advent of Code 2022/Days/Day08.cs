using Advent_of_Code_2022.Extensions;
using Advent_of_Code_2022.Lib;
using MoreLinq;

namespace Advent_of_Code_2022.Days;

public class Day08 : DayBase<Grid<int>, int>
{
    public override string Name => "Day 8: Treetop Tree House";

    public override Grid<int> ParseInput(IEnumerable<string> lines)
        => lines.ToGrid(c => int.Parse(c.ToString()));

    public override int RunPart1(Grid<int> input)
    {
        var edgeItemCount = input.RowCount * 2 + input.ColCount * 2 - 4;
        
        var innerItemCount = input.Inner().Count(i =>
        {
            return i.Tops().All(j => j.Value < i.Value)
                   || i.Rights().All(j => j.Value < i.Value)
                   || i.Bottoms().All(j => j.Value < i.Value)
                   || i.Lefts().All(j => j.Value < i.Value);
        });

        return edgeItemCount + innerItemCount;
    }

    public override int RunPart2(Grid<int> input)
    {
        var max = input.All().Max(i =>
        {
            return i.Tops().TakeUntil(j => j.Value >= i.Value).Count()
                   * i.Rights().TakeUntil(j => j.Value >= i.Value).Count()
                   * i.Bottoms().TakeUntil(j => j.Value >= i.Value).Count()
                   * i.Lefts().TakeUntil(j => j.Value >= i.Value).Count();
        });

        return max;
    }
}
