using Advent_of_Code_2022.Lib;

namespace Advent_of_Code_2022.Days;

public class Day14 : DayBase<Day14.Cave, int>
{
    private const char Rock = '#';
    private const char Sand = 'o';

    private static readonly Vec2<int> Start = new(500, 0);
    private static readonly Vec2<int> Down = new(0, 1);
    private static readonly Vec2<int> Left = new(-1, 1);
    private static readonly Vec2<int> Right = new(1, 1);

    public override string Name => "Day 14: Regolith Reservoir";
    
    public override Cave ParseInput(IEnumerable<string> lines)
    {
        var dictionary = new Dictionary<Vec2<int>, char>();
        foreach (var line in lines)
        {
            var parts = line.Split(" -> ").Select(Vec2<int>.Parse).ToList();
            var lastPart = parts[0];

            foreach (var part in parts.Skip(1))
            {
                foreach (var vec in lastPart.AllTo(part))
                {
                    dictionary[vec] = Rock;
                }

                lastPart = part;
            }
        }

        var height = dictionary.Select(v => v.Key.Y).Max();

        return new Cave
        {
            Height = height,
            Blocks = dictionary
        };
    }

    public override int RunPart1(Cave input)
    {
        var count = 0;
        while (AddSand(input, false))
            count++;
        return count;
    }

    public override int RunPart2(Cave input)
    {
        var count = 0;
        while (AddSand(input, true))
            count++;
        return count;
    }

    private static bool AddSand(Cave cave, bool floor)
    {
        var vec = Start;
        while (CanMove(cave, vec, floor))
        {
            while (!cave.Blocks.ContainsKey(vec + Down) && (vec + Down).Y < cave.Height + 2)
            {
                vec += Down;
                if (vec.Y > cave.Height && !floor)
                    return false;
            }

            if (!cave.Blocks.ContainsKey(vec + Left) && (vec + Down).Y < cave.Height + 2)
            {
                vec += Left;
            }
            else if (!cave.Blocks.ContainsKey(vec + Right) && (vec + Down).Y < cave.Height + 2)
            {
                vec += Right;
            }
            else if ((vec + Down).Y < cave.Height + 2)
            {
                cave.Blocks.Add(vec, Sand);
                return true;
            }

            if (vec.Y >= cave.Height && !floor)
                return false;
        }

        return cave.Blocks.TryAdd(vec, Sand);
    }

    private static bool CanMove(Cave cave, Vec2<int> pos, bool floor)
    {
        if (cave.Blocks.ContainsKey(Start))
            return false;
        
        if (floor && (pos + Down).Y >= cave.Height + 2)
            return false;
        
        return !cave.Blocks.ContainsKey(pos + Down)
               || !cave.Blocks.ContainsKey(pos + Left)
               || !cave.Blocks.ContainsKey(pos + Right);
    }

    public struct Cave
    {
        public required int Height { get; init; }
        public required Dictionary<Vec2<int>, char> Blocks { get; init; }
    }
}
