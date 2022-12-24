using System.Diagnostics;
using Advent_of_Code_2022.Extensions;
using Advent_of_Code_2022.Lib;

namespace Advent_of_Code_2022.Days;

public class Day24 : DayBase<Day24.Input, int>
{
    private static readonly Vec2<int> DirRight = new(1, 0);
    private static readonly Vec2<int> DirDown = new(0, 1);
    private static readonly Vec2<int> DirLeft = new(-1, 0);
    private static readonly Vec2<int> DirUp = new(0, -1);

    public override string Name => "Day 24: Blizzard Basin";
    
    public override Input ParseInput(IEnumerable<string> lines)
    {
        var lineList = lines.ToList();
        var rows = lineList.Count - 2;
        var columns = lineList[0].Length - 2;
        var lcm = new[] { rows, columns }.Lcm();

        var blizzards = lineList.Skip(1).SkipLast(1).SelectMany((l, y) =>
        {
            return l.Skip(1).SkipLast(1).Select((c, x) => c switch
            {
                '.' => null,
                '>' => new Blizzard(new Vec2<int>(x, y), DirRight, rows, columns),
                'v' => new Blizzard(new Vec2<int>(x, y), DirDown, rows, columns),
                '<' => new Blizzard(new Vec2<int>(x, y), DirLeft, rows, columns),
                '^' => new Blizzard(new Vec2<int>(x, y), DirUp, rows, columns),
                _ => throw new UnreachableException()
            });
        }).Where(b => b is not null).Select(b => b!).ToList();

        var positionsBase = new HashSet<Vec2<int>>();
        for (var y = 0; y < rows; y++)
        {
            for (var x = 0; x < columns; x++)
            {
                positionsBase.Add(new Vec2<int>(x, y));
            }
        }
        
        var states = new HashSet<Vec2<int>>[lcm];
        for (var i = 0; i < lcm; i++)
        {
            var positions = new HashSet<Vec2<int>>(positionsBase);
            states[i] = positions;

            foreach (var blizzard in blizzards)
            {
                positions.Remove(blizzard.Position);
                blizzard.Move();
            }
        }

        var start = new Vec2<int>(lineList.First().Skip(1).IndexOf('.'), -1);
        var end = new Vec2<int>(lineList.Last().Skip(1).IndexOf('.'), rows);

        return new Input(states, start, end);
    }

    public override int RunPart1(Input input)
        => FindShortestPath(input.Start, input.End, 0, input);

    public override int RunPart2(Input input)
    {
        var minutes = FindShortestPath(input.Start, input.End, 0, input);
        minutes = FindShortestPath(input.End, input.Start, minutes, input);
        return FindShortestPath(input.Start, input.End, minutes, input);
    }

    private static int FindShortestPath(Vec2<int> start, Vec2<int> end, int minutes, Input input)
    {
        var queue = new Queue<State>();
        queue.Enqueue(new State(start, minutes));

        var best = new Dictionary<State, int>();
        while (queue.Count > 0)
        {
            var state = queue.Dequeue();
            if (state.Position == end)
                return state.Minute;

            var stateLookup = state with { Minute = state.Minute % input.States.Length };
            if (state.Minute >= best.GetValueOrDefault(stateLookup, int.MaxValue))
                continue;

            best[stateLookup] = state.Minute;
            var freePositions = input.States[(state.Minute + 1) % input.States.Length];
            if (state.Position == start || freePositions.Contains(state.Position))
                queue.Enqueue(state with { Minute = state.Minute + 1});

            var neighbors = GetNeighbors(state.Position)
                .Where(p => p == end || freePositions.Contains(p))
                .Select(p => new State(p, state.Minute + 1));
            queue.EnqueueAll(neighbors);
        }

        throw new UnreachableException();
    }

    private static IEnumerable<Vec2<int>> GetNeighbors(Vec2<int> position)
    {
        return new[]
        {
            position + DirRight,
            position + DirDown,
            position + DirLeft,
            position + DirUp
        };
    }

    public record Input(HashSet<Vec2<int>>[] States, Vec2<int> Start, Vec2<int> End);

    private class Blizzard
    {
        public Vec2<int> Position { get; private set; }

        private readonly Vec2<int> _direction;
        private readonly int _rows;
        private readonly int _columns;

        public Blizzard(Vec2<int> position, Vec2<int> direction, int rows, int columns)
        {
            Position = position;
            _direction = direction;
            _rows = rows;
            _columns = columns;
        }

        public void Move()
        {
            Position = new Vec2<int>((Position.X + _direction.X).Mod(_columns), (Position.Y + _direction.Y).Mod(_rows));
        }
    }

    private record State(Vec2<int> Position, int Minute);
}
