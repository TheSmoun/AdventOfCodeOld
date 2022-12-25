using System.Diagnostics;

namespace Advent_of_Code_2022.Days;

public class Day25 : DayBase<IEnumerable<long>, string>
{
    public override string Name => "Day 25: Full of Hot Air";
    
    public override IEnumerable<long> ParseInput(IEnumerable<string> lines)
        => lines.Select(StafuToLong);

    public override string RunPart1(IEnumerable<long> input)
        => LongToStafu(input.Sum());

    public override string RunPart2(IEnumerable<long> input)
        => "Merry Christmas";

    private static long StafuToLong(string stafu)
    {
        var result = 0L;

        var place = 1L;
        for (var i = stafu.Length - 1; i >= 0; i--, place *= 5)
        {
            result += place * stafu[i] switch
            {
                '=' => -2,
                '-' => -1,
                '0' => 0,
                '1' => 1,
                '2' => 2,
                _ => throw new UnreachableException()
            };
        }

        return result;
    }

    private static string LongToStafu(long number)
    {
        var digits = new List<long>();
        while (number != 0L)
        {
            digits.Add(number % 5);
            number /= 5;
        }

        var digitCount = digits.Count;
        for (var i = 0; i < digitCount; i++)
            digits.Add(0L);

        for (var i = 0; i < digitCount; i++)
        {
            if (digits[i] >= 3)
            {
                digits[i] -= 5;
                digits[i + 1]++;
            }
        }

        return digits.Aggregate(string.Empty, (current, digit) => digit switch
        {
            -2L => '=',
            -1L => '-',
            0L => '0',
            1L => '1',
            2L => '2',
            _ => throw new UnreachableException()
        } + current).TrimStart('0');
    }
}
