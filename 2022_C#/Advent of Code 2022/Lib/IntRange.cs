using System.Diagnostics;

namespace Advent_of_Code_2022.Lib;

[DebuggerDisplay("{Start} -> {End}")]
public struct IntRange
{
    public int Start { get; }
    public int End { get; }
    
    public IntRange(int start, int end)
    {
        Start = start;
        End = end;
    }
}
