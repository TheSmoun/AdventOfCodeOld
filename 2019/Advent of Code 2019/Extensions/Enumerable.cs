using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using AoC2019.Days;
using AoC2019.Lib;

namespace AoC2019.Extensions
{
    public static class EnumerableEx
    {
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

        public static IEnumerable<long> Range(long start, long count)
        {
            for (var i = 0; i < count; i++)
                yield return start + i;
        }

        public static IntComputer ToIntComputer(this IEnumerable<long> memory, params long[] input)
        {
            return new IntComputer(memory, BlockingCollection(input));
        }

        public static BlockingCollection<T> BlockingCollection<T>(params T[] input)
        {
            return new BlockingCollection<T>(new ConcurrentQueue<T>(input));
        }

        public static Queue<T> ToQueue<T>(this IEnumerable<T> input)
        {
            return new Queue<T>(input);
        }

        public static IEnumerable<T> DequeueOnce<T>(Queue<T> input)
        {
            if (input.Count > 0)
                yield return input.Dequeue();
        }

        public static DayBase Day(this IEnumerable<DayBase> input, int number)
        {
            if (number < 1 || number > 25)
                throw new ArgumentOutOfRangeException(nameof(number));

            return input.Skip(number - 1).First();
        }
    }
}
