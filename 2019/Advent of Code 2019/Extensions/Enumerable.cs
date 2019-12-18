using System;
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

        public static IEnumerable<(T, T)> Pairs<T>(this IEnumerable<T> input)
        {
            var a = input as T[] ?? input.ToArray();
            return a.SelectMany(x => a, (x, y) => (x, y)).Where(t => !ReferenceEquals(t.x, t.y));
        }

        public static IEnumerable<T> RepeatItems<T>(this IEnumerable<T> input, int times)
        {
            foreach (var item in input)
            {
                for (var i = 0; i < times; i++)
                {
                    yield return item;
                }
            }
        }

        public static IntComputer ToIntComputer(this IEnumerable<long> memory)
        {
            return new IntComputer(memory);
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

        public static void EnqueueAll<T>(this Queue<T> queue, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                queue.Enqueue(item);
            }
        }

        public static int ToInt(this IEnumerable<int> input)
        {
            return int.Parse(string.Concat(input));
        }

        public static void Print<T>(this IEnumerable<T> input, ConsoleColor? color = null)
        {
            if (color.HasValue)
                Console.ForegroundColor = color.Value;

            var a = input as T[] ?? input.ToArray();
            Console.Write("[");
            for (var i = 0; i < a.Length; i++)
            {
                Console.Write(a[i]);
                if (i < a.Length - 1)
                    Console.Write(", ");
            }
            Console.WriteLine("]");

            if (color.HasValue)
                Console.ResetColor();
        }

        public static DayBase Day(this IEnumerable<DayBase> input, int number)
        {
            if (number < 1 || number > 25)
                throw new ArgumentOutOfRangeException(nameof(number));

            return input.Skip(number - 1).First();
        }
    }
}
