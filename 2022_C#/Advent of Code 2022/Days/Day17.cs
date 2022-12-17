using System.Diagnostics;
using Advent_of_Code_2022.Extensions;
using Advent_of_Code_2022.Lib;

namespace Advent_of_Code_2022.Days;

public class Day17 : DayBase<char[], long>
{
    private const int TotalWidth = 7;

    private static readonly Vec2<long> Left = new(-1, 0);
    private static readonly Vec2<long> Right = new(1, 0);
    private static readonly Vec2<long> Down = new(0, 1);
    private static readonly Func<long, RockShape>[] RockFactories = {
        RockShape.NewShape0,
        RockShape.NewShape1,
        RockShape.NewShape2,
        RockShape.NewShape3,
        RockShape.NewShape4
    };

    public override string Name => "Day 17: Pyroclastic Flow";

    public override char[] ParseInput(IEnumerable<string> lines)
        => lines.First().ToCharArray();

    public override long RunPart1(char[] input)
    {
        var totalHeight = 0L;
        var occupiedPositions = new HashSet<Vec2<long>>();
        var rockFactoryIndex = 0;
        var jetIndex = 0;
        
        for (var i = 0; i < 2022; i++)
        {
            var rock = RockFactories[rockFactoryIndex](totalHeight);
            rockFactoryIndex = (rockFactoryIndex + 1) % RockFactories.Length;

            var canMove = true;
            while (canMove)
            {
                (canMove, totalHeight) = rock.Move(input[jetIndex], totalHeight, occupiedPositions);
                jetIndex = (jetIndex + 1) % input.Length;
            }
        }

        return Math.Abs(totalHeight);
    }

    public override long RunPart2(char[] input)
    {
        const long rocks = 1000000000000L;
        var cache = new Dictionary<(int, int), (long, long)>();
        
        var totalHeight = 0L;
        var occupiedPositions = new HashSet<Vec2<long>>();
        var rockFactoryIndex = 0;
        var jetIndex = 0;
        
        for (var i = 0L; i < rocks; i++)
        {
            if (cache.ContainsKey((rockFactoryIndex, jetIndex)))
            {
                var (r, height) = cache[(rockFactoryIndex, jetIndex)];
                var (diff, mod) = (rocks - i).DiffMod(r - i);
                if (mod == 0)
                    return Math.Abs(totalHeight) + (height - Math.Abs(totalHeight)) * diff;
            }
            
            cache[(rockFactoryIndex, jetIndex)] = (i, Math.Abs(totalHeight));
            
            var rock = RockFactories[rockFactoryIndex](totalHeight);
            rockFactoryIndex = (rockFactoryIndex + 1) % RockFactories.Length;

            var canMove = true;
            while (canMove)
            {
                (canMove, totalHeight) = rock.Move(input[jetIndex], totalHeight, occupiedPositions);
                jetIndex = (jetIndex + 1) % input.Length;
            }
        }

        throw new UnreachableException();
    }

    public class RockShape
    {
        public Vec2<long> Position { get; set; }
        public Vec2<long>[] RelativePositions { get; }
        public int Width { get; }
        public int Height { get; }

        private RockShape(Vec2<long> position, Vec2<long>[] relativePositions, int width, int height)
        {
            Position = position;
            RelativePositions = relativePositions;
            Width = width;
            Height = height;
        }
        
        public (bool, long) Move(char jet, long totalHeight, HashSet<Vec2<long>> occupiedPositions)
        {
            if (jet == '<')
                MoveLeft(occupiedPositions);
            else
                MoveRight(occupiedPositions);
            
            return MoveDown(totalHeight, occupiedPositions);
        }
        
        private void MoveLeft(HashSet<Vec2<long>> occupiedPositions)
        {
            if (Position.X <= 0)
                return;

            CheckAndMove(Position + Left, occupiedPositions);
        }

        private void MoveRight(HashSet<Vec2<long>> occupiedPositions)
        {
            if (Position.X + Width + 1 > TotalWidth)
                return;

            CheckAndMove(Position + Right, occupiedPositions);
        }

        private void CheckAndMove(Vec2<long> nextPosition, HashSet<Vec2<long>> occupiedPositions)
        {
            if (Collides(nextPosition, occupiedPositions))
                return;
            
            Position = nextPosition;
        }

        private (bool, long) MoveDown(long totalHeight, HashSet<Vec2<long>> occupiedPositions)
        {
            var nextPosition = Position + Down;
            if (nextPosition.Y + Height >= totalHeight && Collides(nextPosition, occupiedPositions))
            {
                foreach (var relativePosition in RelativePositions)
                {
                    occupiedPositions.Add(Position + relativePosition);
                }
                
                return (false, occupiedPositions.Min(p => p.Y));
            }

            Position = nextPosition;
            return (true, totalHeight);
        }

        private bool Collides(Vec2<long> nextPosition, HashSet<Vec2<long>> occupiedPositions)
        {
            foreach (var relativePosition in RelativePositions)
            {
                var positionToCheck = nextPosition + relativePosition;
                if (positionToCheck.Y == 0 || occupiedPositions.Contains(positionToCheck))
                    return true;
            }

            return false;
        }

        public static RockShape NewShape0(long totalHeight)
        {
            const int height = 1;
            var position = GetNewPosition(totalHeight, height);
            var relativePositions = new Vec2<long>[]
            {
                new(0, 0),
                new(1, 0),
                new(2, 0),
                new(3, 0)
            };

            return new RockShape(position, relativePositions, 4, height);
        }

        public static RockShape NewShape1(long totalHeight)
        {
            const int height = 3;
            var position = GetNewPosition(totalHeight, height);
            var relativePositions = new Vec2<long>[]
            {
                new(1, 0),
                new(0, 1),
                new(1, 1),
                new(2, 1),
                new(1, 2)
            };

            return new RockShape(position, relativePositions, 3, height);
        }

        public static RockShape NewShape2(long totalHeight)
        {
            const int height = 3;
            var position = GetNewPosition(totalHeight, height);
            var relativePositions = new Vec2<long>[]
            {
                new(2, 0),
                new(2, 1),
                new(0, 2),
                new(1, 2),
                new(2, 2)
            };

            return new RockShape(position, relativePositions, 3, height);
        }

        public static RockShape NewShape3(long totalHeight)
        {
            const int height = 4;
            var position = GetNewPosition(totalHeight, height);
            var relativePositions = new Vec2<long>[]
            {
                new(0, 0),
                new(0, 1),
                new(0, 2),
                new(0, 3)
            };

            return new RockShape(position, relativePositions, 1, height);
        }

        public static RockShape NewShape4(long totalHeight)
        {
            const int height = 2;
            var position = GetNewPosition(totalHeight, height);
            var relativePositions = new Vec2<long>[]
            {
                new(0, 0),
                new(1, 0),
                new(0, 1),
                new(1, 1)
            };

            return new RockShape(position, relativePositions, 2, height);
        }

        private static Vec2<long> GetNewPosition(long totalHeight, long height)
        {
            return new Vec2<long>(2, totalHeight - 3 - height);
        }
    }
}
