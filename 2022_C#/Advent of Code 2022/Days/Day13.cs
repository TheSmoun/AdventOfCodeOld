using System.Diagnostics;
using MoreLinq;

namespace Advent_of_Code_2022.Days;

public class Day13 : DayBase<IEnumerable<(Day13.IPacket, Day13.IPacket)>, int>
{
    public override string Name => "Day 13: Distress Signal";
    
    public override IEnumerable<(IPacket, IPacket)> ParseInput(IEnumerable<string> lines)
    {
        return lines.Split(string.Empty)
            .Select(pair =>
            {
                var pairList = pair.ToList();
                var first = ParsePacket(pairList[0]!);
                var second = ParsePacket(pairList[1]!);
                return (first, second);
            });
    }

    public override int RunPart1(IEnumerable<(IPacket, IPacket)> input)
        => input
            .Select((packets, i) => packets.Item1.CompareTo(packets.Item2) <= 0 ? i + 1 : 0)
            .Sum();

    public override int RunPart2(IEnumerable<(IPacket, IPacket)> input)
    {
        var firstDividerPacket = GetDividerPacket(2);
        var secondDividerPacket = GetDividerPacket(6);

        var allPackets = new List<IPacket> { firstDividerPacket, secondDividerPacket };
        allPackets.AddRange(input.SelectMany(t => new List<IPacket> {t.Item1, t.Item2}));
        allPackets.Sort();

        return (allPackets.IndexOf(firstDividerPacket) + 1) * (allPackets.IndexOf(secondDividerPacket) + 1);
    }

    private static IPacket ParsePacket(string line)
    {
        line = line.Substring(1, line.Length - 2);
        var root = new ListPacket(null);
        var current = root;

        while (line.Length > 0)
        {
            switch (line[0])
            {
                case '[':
                    var newPacket = new ListPacket(current);
                    current.Add(newPacket);
                    current = newPacket;
                    line = line[1..];
                    break;
                case ']':
                    current = current.Parent!;
                    line = line[1..];
                    break;
                case ',':
                    line = line[1..];
                    break;
                default:
                    var index = line.IndexOfAny(new[] { ',', ']' });
                    if (index < 0)
                    {
                        current.Add(new IntPacket(int.Parse(line)));
                        line = string.Empty;
                    }
                    else
                    {
                        var number = line[..index];
                        current.Add(new IntPacket(int.Parse(number)));
                        line = line[index..];
                    }
                    break;
            }
        }

        return root;
    }

    private static IPacket GetDividerPacket(int number)
    {
        var outer = new ListPacket(null);
        var inner = new ListPacket(outer);
        outer.Add(inner);
        inner.Add(new IntPacket(number));
        return outer;
    }
    
    public interface IPacket : IComparable<IPacket> { }

    public class ListPacket : List<IPacket>, IPacket
    {
        public ListPacket? Parent { get; }

        public ListPacket(ListPacket? parent)
        {
            Parent = parent;
        }
        
        public int CompareTo(IPacket? other)
        {
            if (other is IntPacket intPacket)
                return CompareTo(new ListPacket(null) { intPacket });
            if (other is not ListPacket listPacket)
                throw new UnreachableException();

            var length = Math.Min(Count, listPacket.Count);
            for (var i = 0; i < length; i++)
            {
                var result = this[i].CompareTo(listPacket[i]);
                if (result != 0)
                    return result;
            }

            if (Count < listPacket.Count)
                return -1;
            if (Count > listPacket.Count)
                return 1;
            return 0;
        }

        public override string ToString()
        {
            return $"[{string.Join(',', AsReadOnly())}]";
        }
    }
    
    public class IntPacket : IPacket
    {
        public int Value { get; }

        public IntPacket(int value)
        {
            Value = value;
        }
        
        public int CompareTo(IPacket? other)
        {
            return other switch
            {
                ListPacket listPacket => new ListPacket(null) {this}.CompareTo(listPacket),
                IntPacket intPacket => Value.CompareTo(intPacket.Value),
                _ => throw new UnreachableException()
            };
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
