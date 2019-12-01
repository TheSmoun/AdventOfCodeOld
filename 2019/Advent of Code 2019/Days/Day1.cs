using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2019.Days
{
    public sealed class Day1 : DayBase<IEnumerable<int>, int>
    {
        protected override string Name => "Day 1: The Tyranny of the Rocket Equation";

        protected override IEnumerable<int> ParseInput(IEnumerable<string> lines)
        {
            return lines.Select(l => Convert.ToInt32(l));
        }

        protected override int RunPart1(IEnumerable<int> input)
        {
            return input.Select(CalcFuel).Sum();
        }

        protected override int RunPart2(IEnumerable<int> input)
        {
            var fuelMasses = new List<int>();

            foreach (var moduleMass in input)
            {
                var masses = new LinkedList<int>();
                masses.AddLast(moduleMass);

                while (masses.Last.Value > 0)
                {
                    masses.AddLast(Math.Max(0, CalcFuel(masses.Last.Value)));
                }

                fuelMasses.Add(masses.Skip(1).Sum());
            }

            return fuelMasses.Sum();
        }

        private static int CalcFuel(int moduleMass) => moduleMass / 3 - 2;
    }
}
