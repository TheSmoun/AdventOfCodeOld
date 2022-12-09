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
                var diff = head - tail;
                if (Math.Abs(diff.X) <= 1 && Math.Abs(diff.Y) <= 1)
                    continue;

                tail = new Vec2<int>(tail.X + Math.Sign(head.X - tail.X), tail.Y + Math.Sign(head.Y - tail.Y));
                positions.Add(tail);
            }
        }

        return positions.Count;
    }

    public override int RunPart2(IEnumerable<Instruction> input)
    {
        throw new NotImplementedException();
    }

    public record Instruction(Vec2<int> Direction, int Amount);
}
