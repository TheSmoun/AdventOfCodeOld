using System;
using System.Collections.Generic;
using System.Linq;
using AoC2019.Extensions;
using MoreLinq;

namespace AoC2019.Days
{
    public sealed class Day16 : DayBase<int[], int>
    {
        public override string Name => "Day 16: Flawed Frequency Transmission";

        public override int[] ParseInput(IEnumerable<string> lines)
        {
            return lines.Single().ToCharArray().Select(c => int.Parse($"{c}")).ToArray();
        }

        public override int RunPart1(int[] input)
        {
            var basePattern = new[] { 0, 1, 0, -1 };
            var patterns = input.Select((_, i) => basePattern.RepeatItems(i + 1).ToArray()).ToArray();

            for (var phase = 0; phase < 100; phase++)
            {
                input = input.Select((item, i) =>
                {
                    var pattern = patterns[i];
                    var result = input.Select((t, j) => t * pattern[(j + 1) % pattern.Length]).Sum();
                    return Math.Abs(result) % 10;
                }).ToArray();
            }

            return input.Take(8).ToInt();
        }

        public override int RunPart2(int[] input)
        {
            var messageOffset = input.Take(7).ToInt();
            input = input.Repeat(10000).ToArray();

            for (var phase = 0; phase < 100; phase++)
            {
                for (var i = 0; i < input.Length - 1; i++)
                {
                    input[i] = (input[i] + input[i + 1]) % 10;
                }
            }

            return input.Skip(messageOffset).Take(8).ToInt();
        }
    }
}
