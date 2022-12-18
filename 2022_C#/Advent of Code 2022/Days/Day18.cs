using Advent_of_Code_2022.Lib;

namespace Advent_of_Code_2022.Days;

public class Day18 : DayBase<HashSet<Vec3<int>>, int>
{
    public override string Name => "Day 18: Boiling Boulders";

    public override HashSet<Vec3<int>> ParseInput(IEnumerable<string> lines)
        => lines.Select(Vec3<int>.Parse).ToHashSet();
    
    public override int RunPart1(HashSet<Vec3<int>> input)
        => input.SelectMany(GetNeighbors).Count(v => !input.Contains(v));

    public override int RunPart2(HashSet<Vec3<int>> input)
    {
        var xRange = Enumerable.Range(input.Min(c => c.X), input.Max(c => c.X) + 1).ToList();
        var yRange = Enumerable.Range(input.Min(c => c.Y), input.Max(c => c.Y) + 1).ToList();
        var zRange = Enumerable.Range(input.Min(c => c.Z), input.Max(c => c.Z) + 1).ToList();

        return input.SelectMany(GetNeighbors)
            .Count(c => IsOutside(input, c, xRange, yRange, zRange));
    }

    private static IEnumerable<Vec3<int>> GetNeighbors(Vec3<int> vec)
    {
        yield return new Vec3<int>(vec.X + 1, vec.Y, vec.Z);
        yield return new Vec3<int>(vec.X - 1, vec.Y, vec.Z);
        yield return new Vec3<int>(vec.X, vec.Y + 1, vec.Z);
        yield return new Vec3<int>(vec.X, vec.Y - 1, vec.Z);
        yield return new Vec3<int>(vec.X, vec.Y, vec.Z + 1);
        yield return new Vec3<int>(vec.X, vec.Y, vec.Z - 1);
    }

    private static bool IsOutside(HashSet<Vec3<int>> cubes, Vec3<int> cube, List<int> xRange, List<int> yRange,
        List<int> zRange)
    {
        if (cubes.Contains(cube))
            return false;

        var checkedCubes = new HashSet<Vec3<int>>();
        var queue = new Queue<Vec3<int>>();
        queue.Enqueue(cube);
        
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (checkedCubes.Contains(current))
                continue;
            
            checkedCubes.Add(current);
            if (!xRange.Contains(current.X) || !yRange.Contains(current.Y) || !zRange.Contains(current.Z))
                return true;
            
            if (!cubes.Contains(current))
            {
                foreach (var neighbor in GetNeighbors(current))
                {
                    queue.Enqueue(neighbor);
                }
            }
        }
        
        return false;
    }
}
