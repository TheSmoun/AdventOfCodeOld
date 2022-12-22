using System.Diagnostics;
using Advent_of_Code_2022.Lib;
using MoreLinq;

namespace Advent_of_Code_2022.Days;

public class Day22 : DayBase<Day22.Input, int>
{
    private static readonly Dictionary<int, WithinFacePredicate> WithinFacePredicates = new()
    {
        { 1, pos => pos.X is >= 51 and <= 100 && pos.Y is >= 1 and <= 50 },
        { 2, pos => pos.X is >= 1 and <= 50 && pos.Y is >= 101 and <= 150 },
        { 3, pos => pos.X is >= 1 and <= 50 && pos.Y is >= 151 and <= 200 },
        { 4, pos => pos.X is >= 51 and <= 100 && pos.Y is >= 51 and <= 100 },
        { 5, pos => pos.X is >= 101 and <= 150 && pos.Y is >= 1 and <= 50 },
        { 6, pos => pos.X is >= 51 and <= 100 && pos.Y is >= 101 and <= 150 }
    };

    private static readonly Dictionary<(int, Vec2<int>), int> NewFaces = new()
    {
        { (1, new Vec2<int>(1, 0)), 5 },
        { (1, new Vec2<int>(0, 1)), 4 },
        { (1, new Vec2<int>(-1, 0)), 2 },
        { (1, new Vec2<int>(0, -1)), 3 },
        { (2, new Vec2<int>(1, 0)), 6 },
        { (2, new Vec2<int>(0, 1)), 3 },
        { (2, new Vec2<int>(-1, 0)), 1 },
        { (2, new Vec2<int>(0, -1)), 4 },
        { (3, new Vec2<int>(1, 0)), 6 },
        { (3, new Vec2<int>(0, 1)), 5 },
        { (3, new Vec2<int>(-1, 0)), 1 },
        { (3, new Vec2<int>(0, -1)), 2 },
        { (4, new Vec2<int>(1, 0)), 5 },
        { (4, new Vec2<int>(0, 1)), 6 },
        { (4, new Vec2<int>(-1, 0)), 2 },
        { (4, new Vec2<int>(0, -1)), 1 },
        { (5, new Vec2<int>(1, 0)), 6 },
        { (5, new Vec2<int>(0, 1)), 4 },
        { (5, new Vec2<int>(-1, 0)), 1 },
        { (5, new Vec2<int>(0, -1)), 3 },
        { (6, new Vec2<int>(1, 0)), 5 },
        { (6, new Vec2<int>(0, 1)), 3 },
        { (6, new Vec2<int>(-1, 0)), 2 },
        { (6, new Vec2<int>(0, -1)), 4 }
    };
    
    private static readonly Dictionary<(int, int), (PositionTransformation, Vec2<int>)> FacingTransformations = new()
    {
        { (1, 5), (pos => new Vec2<int>(pos.X + 1, pos.Y), new Vec2<int>(1, 0)) },
        { (1, 4), (pos => new Vec2<int>(pos.X, pos.Y + 1), new Vec2<int>(0, 1)) },
        { (1, 2), (pos => new Vec2<int>(1, 150 - pos.Y), new Vec2<int>(1, 0)) },
        { (1, 3), (pos => new Vec2<int>(1, 150 + (pos.X - 50)), new Vec2<int>(1, 0)) },
        { (2, 6), (pos => new Vec2<int>(pos.X + 1, pos.Y), new Vec2<int>(1, 0)) },
        { (2, 3), (pos => new Vec2<int>(pos.X, pos.Y + 1), new Vec2<int>(0, 1)) },
        { (2, 1), (pos => new Vec2<int>(51, 150 - pos.Y), new Vec2<int>(1, 0)) },
        { (2, 4), (pos => new Vec2<int>(51, 50 + pos.X), new Vec2<int>(1, 0)) },
        { (3, 6), (pos => new Vec2<int>( pos.Y - 100, 150), new Vec2<int>(0, -1)) },
        { (3, 5), (pos => new Vec2<int>(100 + pos.X, 1), new Vec2<int>(0, 1)) },
        { (3, 1), (pos => new Vec2<int>(pos.Y - 100, 1), new Vec2<int>(0, 1)) },
        { (3, 2), (pos => new Vec2<int>(pos.X, pos.Y - 1), new Vec2<int>(0, -1)) },
        { (4, 5), (pos => new Vec2<int>(pos.Y + 50, 50), new Vec2<int>(0, -1)) },
        { (4, 6), (pos => new Vec2<int>(pos.X, pos.Y + 1), new Vec2<int>(0, 1)) },
        { (4, 2), (pos => new Vec2<int>(pos.Y - 50, 101), new Vec2<int>(0, 1)) },
        { (4, 1), (pos => new Vec2<int>(pos.X, pos.Y - 1), new Vec2<int>(0, -1)) },
        { (5, 6), (pos => new Vec2<int>(100, 150 - pos.Y), new Vec2<int>(-1, 0)) },
        { (5, 4), (pos => new Vec2<int>(100, pos.X - 50), new Vec2<int>(-1, 0)) },
        { (5, 1), (pos => new Vec2<int>(pos.X - 1, pos.Y), new Vec2<int>(-1, 0)) },
        { (5, 3), (pos => new Vec2<int>(pos.X - 100, 200), new Vec2<int>(0, -1)) },
        { (6, 5), (pos => new Vec2<int>(150, 150 - pos.Y), new Vec2<int>(-1, 0)) },
        { (6, 3), (pos => new Vec2<int>(50, pos.X + 100), new Vec2<int>(-1, 0)) },
        { (6, 2), (pos => new Vec2<int>(pos.X - 1, pos.Y), new Vec2<int>(-1, 0)) },
        { (6, 4), (pos => new Vec2<int>(pos.X, pos.Y - 1), new Vec2<int>(0, -1)) }
    };

    private static readonly Dictionary<Vec2<int>, int> Direction2Facing = new()
    {
        { new Vec2<int>(1, 0), 0 },
        { new Vec2<int>(0, 1), 1 },
        { new Vec2<int>(-1, 0), 2 },
        { new Vec2<int>(0, -1), 3 }
    };

    public override string Name => "Day 22: Monkey Map";
    
    public override Input ParseInput(IEnumerable<string> lines)
    {
        var parts = lines.Split(string.Empty).ToArray();

        var mapLines = parts[0].ToList();
        var map = new Dictionary<Vec2<int>, bool>();
        for (var i = 0; i < mapLines.Count; i++)
        {
            var line = mapLines[i]!;
            for (var j = 0; j < line.Length; j++)
            {
                var c = line[j];
                if (c is '.' or '#')
                {
                    map[new Vec2<int>(j + 1, i + 1)] = c == '.';
                }
            }
        }

        var instructionsRaw = parts[1].Single()!;
        var instructions = new List<IInstruction>();
        
        while (instructionsRaw.Length > 0)
        {
            if (instructionsRaw.StartsWith('R'))
            {
                instructions.Add(new TurnInstruction { Clockwise = true });
                instructionsRaw = instructionsRaw[1..];
            }
            else if (instructionsRaw.StartsWith('L'))
            {
                instructions.Add(new TurnInstruction { Clockwise = false });
                instructionsRaw = instructionsRaw[1..];
            }
            else
            {
                var index = instructionsRaw.IndexOfAny(new[] { 'R', 'L' });
                if (index > 0)
                {
                    var amount = int.Parse(instructionsRaw[..index]);
                    instructions.Add(new MoveInstruction { Amount = amount });
                    instructionsRaw = instructionsRaw[index..];
                }
                else
                {
                    var amount = int.Parse(instructionsRaw);
                    instructions.Add(new MoveInstruction { Amount = amount });
                    instructionsRaw = string.Empty;
                }
            }
        }

        return new Input
        {
            Map = map,
            Instructions = instructions
        };
    }

    public override int RunPart1(Input input)
    {
        var position = Enumerable.MinBy(input.Map.Keys.Where(k => k.Y == 1), k => k.X)!;
        var direction = new Vec2<int>(1, 0);

        foreach (var instruction in input.Instructions)
        {
            switch (instruction)
            {
                case TurnInstruction turn:
                    direction = direction.Rotate(turn.Clockwise);
                    break;
                case MoveInstruction move:
                    for (var i = 0; i < move.Amount; i++)
                    {
                        var newPosition = position + direction;
                        if (!input.Map.ContainsKey(newPosition))
                        {
                            if (direction.X > 0)
                            {
                                var row = newPosition.Y;
                                newPosition = Enumerable.MinBy(input.Map.Keys.Where(k => k.Y == row), k => k.X)!;
                            }
                            else if (direction.X < 0)
                            {
                                var row = newPosition.Y;
                                newPosition = Enumerable.MaxBy(input.Map.Keys.Where(k => k.Y == row), k => k.X)!;
                            }
                            else if (direction.Y > 0)
                            {
                                var col = newPosition.X;
                                newPosition = Enumerable.MinBy(input.Map.Keys.Where(k => k.X == col), k => k.Y)!;
                            }
                            else if (direction.Y < 0)
                            {
                                var col = newPosition.X;
                                newPosition = Enumerable.MaxBy(input.Map.Keys.Where(k => k.X == col), k => k.Y)!;
                            }
                        }

                        if (!input.Map.ContainsKey(newPosition))
                            throw new UnreachableException();
                        
                        if (!input.Map[newPosition])
                            break;

                        position = newPosition;
                    }
                    break;
            }
        }
        
        return position.Y * 1000 + position.X * 4 + Direction2Facing[direction];
    }

    public override int RunPart2(Input input)
    {
        var position = Enumerable.MinBy(input.Map.Keys.Where(k => k.Y == 1), k => k.X)!;
        var face = 1;
        var direction = new Vec2<int>(1, 0);

        foreach (var instruction in input.Instructions)
        {
            switch (instruction)
            {
                case TurnInstruction turn:
                    direction = direction.Rotate(turn.Clockwise);
                    break;
                case MoveInstruction move:
                    for (var i = 0; i < move.Amount; i++)
                    {
                        var newPosition = position + direction;
                        var newFace = WithinFacePredicates[face](newPosition) ? face : NewFaces[(face, direction)];
                        var newDirection = direction;
                        if (face != newFace)
                        {
                            (var transformation, newDirection) = FacingTransformations[(face, newFace)];
                            newPosition = transformation(position);
                        }

                        if (!input.Map.ContainsKey(newPosition))
                            throw new UnreachableException();
                        
                        if (!input.Map[newPosition])
                            break;

                        position = newPosition;
                        direction = newDirection;
                        face = newFace;
                    }
                    break;
            }
        }
        
        return position.Y * 1000 + position.X * 4 + Direction2Facing[direction];
    }

    public class Input
    {
        public required Dictionary<Vec2<int>, bool> Map { get; init; }
        public required List<IInstruction> Instructions { get; init; }
    }

    public interface IInstruction { }

    public class MoveInstruction : IInstruction
    {
        public required int Amount { get; init; }
    }

    public class TurnInstruction : IInstruction
    {
        public required bool Clockwise { get; init; }
    }

    private delegate Vec2<int> PositionTransformation(Vec2<int> position);

    private delegate bool WithinFacePredicate(Vec2<int> position);
}
