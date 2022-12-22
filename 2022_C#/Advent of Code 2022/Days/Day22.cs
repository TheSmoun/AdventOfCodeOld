using System.Diagnostics;
using Advent_of_Code_2022.Lib;
using MoreLinq;

namespace Advent_of_Code_2022.Days;

public class Day22 : DayBase<Day22.Input, int>
{
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
        var face = new Cube().Faces[1];
        var position = new Vec2<int>(1, 1);
        var direction = new Vec2<int>(1, 0);
        var positionGlobal = new Vec2<int>(0, 0);

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
                        var (newFace, newPosition, newPositionGlobal, newDirection) = face.Move(position, direction);
                        if (!input.Map[newPositionGlobal])
                            break;

                        position = newPosition;
                        positionGlobal = newPositionGlobal;
                        direction = newDirection;
                        face = newFace;
                    }
                    break;
            }
        }

        return positionGlobal.Y * 1000 + positionGlobal.X * 4 + Direction2Facing[direction];
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

    public class Cube
    {
        public Dictionary<int, CubeFace> Faces { get; } = new();

        public Cube()
        {
            var face1 = new CubeFace12(1, 50, 0);
            var face2 = new CubeFace12(2, 0, 100);
            var face3 = new CubeFace34(3, 0, 150);
            var face4 = new CubeFace34(4, 50, 50);
            var face5 = new CubeFace56(5, 100, 0);
            var face6 = new CubeFace56(6, 50, 100);
            
            Faces.Add(face1.Id, face1);
            Faces.Add(face2.Id, face2);
            Faces.Add(face3.Id, face3);
            Faces.Add(face4.Id, face4);
            Faces.Add(face5.Id, face5);
            Faces.Add(face6.Id, face6);

            face1.Right = face5;
            face1.Down = face4;
            face1.Left = face2;
            face1.Up = face3;

            face2.Right = face6;
            face2.Down = face3;
            face2.Left = face1;
            face2.Up = face4;

            face3.Right = face6;
            face3.Down = face5;
            face3.Left = face1;
            face3.Up = face2;

            face4.Right = face5;
            face4.Down = face6;
            face4.Left = face2;
            face4.Up = face1;

            face5.Right = face6;
            face5.Down = face4;
            face5.Left = face1;
            face5.Up = face3;

            face6.Right = face5;
            face6.Down = face3;
            face6.Left = face2;
            face6.Up = face4;
        }
    }

    public abstract class CubeFace
    {
        public int Id { get; }

        public CubeFace Right { get; set; } = default!;
        public CubeFace Down { get; set; } = default!;
        public CubeFace Left { get; set; } = default!;
        public CubeFace Up { get; set; } = default!;

        protected abstract Vec2<int> DirectionRight { get; }
        protected abstract Vec2<int> DirectionDown { get; }
        protected abstract Vec2<int> DirectionLeft { get; }
        protected abstract Vec2<int> DirectionUp { get; }

        private readonly Vec2<int> _shift;

        protected CubeFace(int id, int xShift, int yShift)
        {
            Id = id;
            _shift = new Vec2<int>(xShift, yShift);
        }

        public (CubeFace, Vec2<int>, Vec2<int>, Vec2<int>) Move(Vec2<int> position, Vec2<int> direction)
        {
            var newPosition = position + direction;

            CubeFace newFace;
            Vec2<int> newDirection;

            if (newPosition.X > 50)
                (newFace, newPosition, newDirection) = (Right, MoveRight(position), DirectionRight);
            else if (newPosition.Y > 50)
                (newFace, newPosition, newDirection) = (Down, MoveDown(position), DirectionDown);
            else if (newPosition.X < 1)
                (newFace, newPosition, newDirection) = (Left, MoveLeft(position), DirectionLeft);
            else if (newPosition.Y < 1)
                (newFace, newPosition, newDirection) = (Up, MoveUp(position), DirectionUp);
            else
                (newFace, newDirection) = (this, direction);

            return (newFace, newPosition, newPosition + _shift, newDirection);
        }

        protected abstract Vec2<int> MoveRight(Vec2<int> pos);
        protected abstract Vec2<int> MoveDown(Vec2<int> pos);
        protected abstract Vec2<int> MoveLeft(Vec2<int> pos);
        protected abstract Vec2<int> MoveUp(Vec2<int> pos);
    }

    private class CubeFace12 : CubeFace
    {
        protected override Vec2<int> DirectionRight => new(1, 0);
        protected override Vec2<int> DirectionDown => new(0, 1);
        protected override Vec2<int> DirectionLeft => new(1, 0);
        protected override Vec2<int> DirectionUp => new(1, 0);

        public CubeFace12(int id, int xShift, int yShift) : base(id, xShift, yShift) { }

        protected override Vec2<int> MoveRight(Vec2<int> pos) => new(1, pos.Y);
        protected override Vec2<int> MoveDown(Vec2<int> pos) => new(pos.X, 1);
        protected override Vec2<int> MoveLeft(Vec2<int> pos) => new(1, 51 - pos.Y);
        protected override Vec2<int> MoveUp(Vec2<int> pos) => new(1, pos.X);
    }

    private class CubeFace34 : CubeFace
    {
        protected override Vec2<int> DirectionRight => new(0, -1);
        protected override Vec2<int> DirectionDown => new(0, 1);
        protected override Vec2<int> DirectionLeft => new(0, 1);
        protected override Vec2<int> DirectionUp => new(0, -1);

        public CubeFace34(int id, int xShift, int yShift) : base(id, xShift, yShift) { }
        
        protected override Vec2<int> MoveRight(Vec2<int> pos) => new(pos.Y, 50);
        protected override Vec2<int> MoveDown(Vec2<int> pos) => new(pos.X, 1);
        protected override Vec2<int> MoveLeft(Vec2<int> pos) => new(pos.Y, 1);
        protected override Vec2<int> MoveUp(Vec2<int> pos) => new(pos.X, 50);
    }

    private class CubeFace56 : CubeFace
    {
        protected override Vec2<int> DirectionRight => new(-1, 0);
        protected override Vec2<int> DirectionDown => new(-1, 0);
        protected override Vec2<int> DirectionLeft => new(-1, 0);
        protected override Vec2<int> DirectionUp => new(0, -1);

        public CubeFace56(int id, int xShift, int yShift) : base(id, xShift, yShift) { }
        
        protected override Vec2<int> MoveRight(Vec2<int> pos) => new(50, 51 - pos.Y);
        protected override Vec2<int> MoveDown(Vec2<int> pos) => new(50, pos.X);
        protected override Vec2<int> MoveLeft(Vec2<int> pos) => new(50, pos.Y);
        protected override Vec2<int> MoveUp(Vec2<int> pos) => new(pos.X, 50);
    }
}
