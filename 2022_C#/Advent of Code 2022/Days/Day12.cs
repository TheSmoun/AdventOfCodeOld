using Advent_of_Code_2022.Extensions;
using Advent_of_Code_2022.Lib;

namespace Advent_of_Code_2022.Days;

public class Day12 : DayBase<Grid<int>, int>
{
    private static readonly int S = 0;
    private static readonly int E = 27;
    
    public override string Name => "Day 12: Hill Climbing Algorithm";
    
    public override Grid<int> ParseInput(IEnumerable<string> lines)
        => lines.ToGrid(c => c switch
        {
            'S' => S,
            'E' => E,
            _ => c - 'a' + 1
        });

    public override int RunPart1(Grid<int> input)
    {
        var start = input.All().First(i => i.Value == S);
        var end = input.All().First(i => i.Value == E);
        return FindShortestPath(input, start, end);
    }

    public override int RunPart2(Grid<int> input)
    {
        var end = input.All().First(i => i.Value == E);
        return input.All()
            .Where(i => i.Value == 1)
            .Min(s => FindShortestPath(input, s, end));
    }

    private static int FindShortestPath(Grid<int> grid, GridItem<int> start, GridItem<int> end)
    {
        var distances = new Grid<int>(grid.RowCount, grid.ColCount, int.MaxValue);

        var queue = new Queue<GridItem<int>>();
        queue.Enqueue(start);
        distances[start.Row, start.Col] = 0;

        while (queue.Count > 0)
        {
            var item = queue.Dequeue();
            var itemDistance = distances[item.Row, item.Col];
            
            foreach (var neighbor in item.Neighbors().Where(n => n.Value <= item.Value + 1))
            {
                var distance = distances[neighbor.Row, neighbor.Col];
                var newDistance = Math.Min(itemDistance + 1, distance);

                if (newDistance < distance)
                {
                    queue.Enqueue(neighbor);
                    distances[neighbor.Row, neighbor.Col] = newDistance;
                }
            }
        }

        return distances[end.Row, end.Col];
    }
}
