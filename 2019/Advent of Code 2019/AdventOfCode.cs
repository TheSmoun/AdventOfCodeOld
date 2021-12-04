using System;
using System.Collections.Generic;
using System.Linq;
using AoC2019.Days;
using AoC2019.Extensions;

namespace AoC2019
{
    public sealed class AdventOfCode
    {
        private static readonly IEnumerable<DayBase> Days = typeof(AdventOfCode).Assembly.GetTypes()
            .Where(t => !t.IsAbstract && typeof(DayBase).IsAssignableFrom(t))
            .OrderBy(t => int.Parse(t.Name.Substring(3)))
            .Select(t => Activator.CreateInstance(t) as DayBase);

        public static void Main(string[] args)
        {
            Days.Last().Run();
            // Days.Day(17).Run();
        }
    }
}
