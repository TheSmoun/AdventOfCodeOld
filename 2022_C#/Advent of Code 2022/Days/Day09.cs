using System.Diagnostics;
using Advent_of_Code_2022.Lib;

namespace Advent_of_Code_2022.Days;

public class Day09 : DayBase<IEnumerable<Day09.Instruction>, int>
{
    public override string Name => "Day 9: Rope Bridge";

    public override IEnumerable<Instruction> ParseInput(IEnumerable<string> lines)
    {
        return lines.Select(l =>
        {
            var amount = int.Parse(l[2..]);
            return l[0] switch
            {
                'R' => new Instruction(new Vec2<int>(1, 0), amount),
                'L' => new Instruction(new Vec2<int>(-1, 0), amount),
                'U' => new Instruction(new Vec2<int>(0, -1), amount),
                'D' => new Instruction(new Vec2<int>(0, 1), amount),
                _ => throw new UnreachableException()
            };
        });
    }

    public override int RunPart1(IEnumerable<Instruction> input)
    {
        var head = new Vec2<int>();
        var tail = new Vec2<int>();
        var positions = new HashSet<Vec2<int>>();
        
        foreach (var instruction in input)
        {
            for (var i = 0; i < instruction.Amount; i++)
            {
                head += instruction.Direction;
                tail = MoveRopePart(head, tail);
                positions.Add(tail);
            }
        }

        return positions.Count;
    }

    public override int RunPart2(IEnumerable<Instruction> input)
    {
        var list = new LinkedList<Vec2<int>>(Enumerable.Range(0, 10).Select(_ => new Vec2<int>()));
        var positions = new HashSet<Vec2<int>>();

        foreach (var instruction in input)
        {
            for (var i = 0; i < instruction.Amount; i++)
            {
                var head = list.First() + instruction.Direction;
                list.First!.Value = head;

                var node = list.First;
                while ((node = node?.Next) is not null)
                {
                    node.Value = MoveRopePart(node.Previous!.Value, node.Value);
                }

                positions.Add(list.Last!.Value);
            }
        }

        return positions.Count;
    }

    private static Vec2<int> MoveRopePart(Vec2<int> prev, Vec2<int> curr)
    {
        var diff = prev - curr;
        if (Math.Abs(diff.X) <= 1 && Math.Abs(diff.Y) <= 1)
            return curr;

        return new Vec2<int>(curr.X + Math.Sign(prev.X - curr.X), curr.Y + Math.Sign(prev.Y - curr.Y));
    }

    public record Instruction(Vec2<int> Direction, int Amount);
}
