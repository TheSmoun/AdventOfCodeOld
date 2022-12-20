using System.Diagnostics;
using Advent_of_Code_2022.Extensions;

namespace Advent_of_Code_2022.Days;

public abstract class DayBase
{
    public abstract void Run();
}

public abstract class DayBase<TInput, TResult> : DayBase
{
    private const string InputsFolderName = "Inputs";

    private readonly string _inputFileName;

    protected DayBase()
    {
        _inputFileName = Path.Combine(InputsFolderName, $"{GetType().Name}.txt");
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

    private static (TParam, TimeSpan) Measure<TParam>(Func<TParam> function)
    {
        var stopwatch = new Stopwatch();

        stopwatch.Start();
        var result = function();
        stopwatch.Stop();

        return (result, stopwatch.Elapsed);
    }

    private static string FormatTime(TimeSpan time)
    {
        var ns = time.TotalNanoseconds;
        if (ns < 1000)
            return $"{ns} ns";

        var ms = time.TotalMicroseconds;
        if (ms < 1000)
            return $"{ms} µs";
        
        ms = time.TotalMilliseconds;
        if (ms < 1000)
            return $"{ms} ms";

        if (time.TotalSeconds < 180)
            return time.TotalSeconds.ToString("##.### 's'");

        // ReSharper disable once ConvertIfStatementToReturnStatement
        if (time.TotalMinutes < 180)
            return time.TotalMinutes.ToString("##.## 'min'");

        return time.TotalHours.ToString("##.## 'h'");
    }

    public abstract string Name { get; }
    public abstract TInput ParseInput(IEnumerable<string> lines);
    public abstract TResult RunPart1(TInput input);
    public abstract TResult RunPart2(TInput input);
}
