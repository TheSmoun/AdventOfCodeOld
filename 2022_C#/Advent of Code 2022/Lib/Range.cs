using System.Diagnostics;
using System.Numerics;

namespace Advent_of_Code_2022.Lib;

[DebuggerDisplay("{Start} -> {End}")]
public struct Range<TNumber> : IComparable<Range<TNumber>>
    where TNumber : INumber<TNumber>
{
    public TNumber Start { get; set; }
    public TNumber End { get; set; }
    
    public Range(TNumber start, TNumber end)
    {
        Start = start;
        End = end;
    }

    public bool Contains(TNumber i)
    {
        return i >= Start && i <= End;
    }

    public int CompareTo(Range<TNumber> other)
    {
        var startComparison = Start.CompareTo(other.Start);
        if (startComparison != 0)
            return startComparison;
        return End.CompareTo(other.End);
    }
}
