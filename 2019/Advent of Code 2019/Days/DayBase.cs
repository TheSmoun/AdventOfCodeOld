using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using AoC2019.Extensions;

namespace AoC2019.Days
{
    public abstract class DayBase
    {
        public abstract void Run();
    }

    public abstract class DayBase<TInput, TResult> : DayBase
    {
        private const long TicksPerMillisecond = 10000;
        private const string InputFolderName = "Inputs";

        private readonly string _inputFileName;

        protected DayBase()
        {
            _inputFileName = Path.Combine(InputFolderName, $"{GetType().Name.ToLower()}.txt");
        }

        public override void Run()
        {
            var lines = File.ReadAllLines(_inputFileName);

            Console.WriteLine();
            ConsoleEx.WriteColoredLine($"   {Name}", ConsoleColor.Magenta);
            Console.WriteLine();

            RunPart("Part 1", RunPart1, lines);
            RunPart("Part 2", RunPart2, lines);
            Console.WriteLine();
        }

        public TInput ParseInput()
        {
            return ParseInput(File.ReadAllLines(_inputFileName));
        }

        private void RunPart(string name, Func<TInput, TResult> partFunction, IEnumerable<string> lines)
        {
            var (input, ti) = Measure(() => ParseInput(lines));
            var (result, tr) = Measure(() => partFunction(input));

            Console.Write($"-> {name}: ");
            ConsoleEx.WriteColoredLine($"{result}", ConsoleColor.Cyan);
            Console.WriteLine($"   Input: {FormatTime(ti)}, {name}: {FormatTime(tr)}, Total: {FormatTime(ti + tr)}");
            Console.WriteLine();
        }

        private static (TParam, long) Measure<TParam>(Func<TParam> function)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            var result = function();
            stopwatch.Stop();

            return (result, stopwatch.ElapsedTicks);
        }

        private static string FormatTime(long ticks)
        {
            if (ticks < TicksPerMillisecond)
                return $"{ticks} ticks";

            var ms = ticks / TicksPerMillisecond;
            if (ms < 1000)
                return $"{ms} ms";

            var ts = TimeSpan.FromMilliseconds(ms);
            if (ts.TotalSeconds < 180)
                return ts.TotalSeconds.ToString("##.### 's'");

            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (ts.TotalMinutes < 180)
                return ts.TotalMinutes.ToString("##.## 'min'");

            return ts.TotalHours.ToString("##.## 'h'");
        }

        public abstract string Name { get; }
        public abstract TInput ParseInput(IEnumerable<string> lines);
        public abstract TResult RunPart1(TInput input);
        public abstract TResult RunPart2(TInput input);
    }
}
