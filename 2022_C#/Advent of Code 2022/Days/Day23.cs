using Advent_of_Code_2022.Lib;

namespace Advent_of_Code_2022.Days;

public class Day23 : DayBase<HashSet<Vec2<int>>, int>
{
    private static readonly IEnumerable<Vec2<int>> NeighborsEast = new[]
    {
        new Vec2<int>(1, -1),
        new Vec2<int>(1, 0),
        new Vec2<int>(1, 1)
    };
    
    private static readonly IEnumerable<Vec2<int>> NeighborsSouth = new[]
    {
        new Vec2<int>(-1, 1),
        new Vec2<int>(0, 1),
        new Vec2<int>(1, 1)
    };
    
    private static readonly IEnumerable<Vec2<int>> NeighborsWest = new[]
    {
        new Vec2<int>(-1, -1),
        new Vec2<int>(-1, 0),
        new Vec2<int>(-1, 1)
    };
    
    private static readonly IEnumerable<Vec2<int>> NeighborsNorth = new[]
    {
        new Vec2<int>(-1, -1),
        new Vec2<int>(0, -1),
        new Vec2<int>(1, -1)
    };

    private static readonly IEnumerable<Vec2<int>> AllNeighbors = NeighborsEast
        .Concat(NeighborsSouth).Concat(NeighborsWest).Concat(NeighborsNorth).ToHashSet();

    private static readonly Vec2<int> MoveEast = new(1, 0);
    private static readonly Vec2<int> MoveSouth = new(0, 1);
    private static readonly Vec2<int> MoveWest = new(-1, 0);
    private static readonly Vec2<int> MoveNorth = new(0, -1);

    private static readonly List<(IEnumerable<Vec2<int>>, Vec2<int>)> MoveProposals = new()
    {
        (NeighborsNorth, MoveNorth),
        (NeighborsSouth, MoveSouth),
        (NeighborsWest, MoveWest),
        (NeighborsEast, MoveEast)
    };

    public override string Name => "Day 23: Unstable Diffusion";
    
    public override HashSet<Vec2<int>> ParseInput(IEnumerable<string> lines)
    {
        var elves = new HashSet<Vec2<int>>();
        var lineList = lines.ToList();
        
        for (var y = 0; y < lineList.Count; y++)
        {
            var line = lineList[y];
            for (var x = 0; x < line.Length; x++)
            {
                if (line[x] == '#')
                    elves.Add(new Vec2<int>(x, y));
            }
        }

        return elves;
    }

    public override int RunPart1(HashSet<Vec2<int>> input)
    {
        for (var i = 0; i < 10; i++)
            (input, _) = SimulateRound(input, i);

        var minX = input.Min(e => e.X);
        var maxX = input.Max(e => e.X);
        var minY = input.Min(e => e.Y);
        var maxY = input.Max(e => e.Y);
        
        var size = (maxX - minX + 1) * (maxY - minY + 1);
        return size - input.Count;
    }

    public override int RunPart2(HashSet<Vec2<int>> input)
    {
        var round = 0;
        var moved = true;
        while (moved)
            (input, moved) = SimulateRound(input, round++);
        
        return round;
    }

    private static (HashSet<Vec2<int>>, bool) SimulateRound(HashSet<Vec2<int>> elves, int round)
    {
        var movePlans = new Dictionary<Vec2<int>, Vec2<int>>();
        var targetCounts = new Dictionary<Vec2<int>, int>();

        foreach (var elf in elves)
        {
            var newPos = elf;
            if (AllNeighbors.Any(n => elves.Contains(elf + n)))
            {
                for (var i = 0; i < MoveProposals.Count; i++)
                {
                    var (neighbors, move) = MoveProposals[(round + i) % MoveProposals.Count];
                    if (!neighbors.Any(n => elves.Contains(elf + n)))
                    {
                        newPos += move;
                        break;
                    }
                }
            }

            movePlans[elf] = newPos;
            
            if (targetCounts.ContainsKey(newPos))
                targetCounts[newPos] += 1;
            else
                targetCounts[newPos] = 1;
        }

        var moved = false;
        var newPositions = new HashSet<Vec2<int>>();
        foreach (var (oldPosition, newPosition) in movePlans)
        {
            newPositions.Add(targetCounts[newPosition] == 1 ? newPosition : oldPosition);
            if (targetCounts[newPosition] == 1 && newPosition != oldPosition)
                moved = true;
        }

        return (newPositions, moved);
    }
}
