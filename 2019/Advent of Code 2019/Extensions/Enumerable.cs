using System;
using System.Collections.Generic;
using System.Linq;
using AoC2019.Days;

namespace AoC2019.Extensions
{
    public static class EnumerableEx
    {
        public static IEnumerable<T> Cycle<T>(this IEnumerable<T> input)
        {
            var enumerable = input as T[] ?? input.ToArray();
            while (true)
            {
                foreach (var t in enumerable)
                {
                    yield return t;
                }
            }
        }

        public static IEnumerable<T> Sequence<T>(this T start, Func<T, T> f)
        {
            yield return start;
            var value = start;

            while (true)
            {
                value = f(value);
                yield return value;
            }
        }

        public static DayBase Day(this IEnumerable<DayBase> input, int number)
        {
            if (number < 1 || number > 25)
                throw new ArgumentOutOfRangeException(nameof(number));

            return input.Skip(number - 1).First();
        }
    }
}
