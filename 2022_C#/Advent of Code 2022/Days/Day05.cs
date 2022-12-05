using System.Text.RegularExpressions;
using MoreLinq;

namespace Advent_of_Code_2022.Days;

public class Day05 : DayBase<Day05.Input, string>
{
    private const string Pattern = @"^move (\d+) from (\d+) to (\d+)$";
    private static readonly Regex Regex = new(Pattern);
    
    public override string Name => "Day 5: Supply Stacks";
    
    public override Input ParseInput(IEnumerable<string> lines)
    {
        var parts = lines.Split(string.Empty).ToList();
        var stacksInput = parts[0].Reverse().ToList();
        var instructionsInput = parts[1];

        var stackCount = int.Parse(stacksInput.First().Split("   ").Last().Trim());
        var stacks = Enumerable.Range(0, stackCount).Select(_ => new Stack<char>()).ToList();

        foreach (var line in stacksInput.Skip(1))
        {
            for (var i = 0; i < stackCount; i++)
            {
                var container = line[1 + i * 4];
                if (container != ' ')
                    stacks[i].Push(container);
            }
        }

        var instructions = instructionsInput.Select(i =>
        {
            var match = Regex.Match(i);
            return new Instruction(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value),
                int.Parse(match.Groups[3].Value));
        }).ToList();

        return new Input(stacks, instructions);
    }

    public override string RunPart1(Input input)
    {
        foreach (var instruction in input.Instructions)
        {
            var from = input.Stacks[instruction.From - 1];
            var to = input.Stacks[instruction.To - 1];
            
            Enumerable.Range(0, instruction.Amount)
                .Select(_ => from.Pop()).ForEach(c => to.Push(c));
        }

        return string.Join(string.Empty, input.Stacks.Select(s => s.Peek()));
    }

    public override string RunPart2(Input input)
    {
        foreach (var instruction in input.Instructions)
        {
            var from = input.Stacks[instruction.From - 1];
            var to = input.Stacks[instruction.To - 1];
            
            Enumerable.Range(0, instruction.Amount)
                .Select(_ => from.Pop()).Reverse().ForEach(c => to.Push(c));
        }

        return string.Join(string.Empty, input.Stacks.Select(s => s.Peek()));
    }
    
    public record Input(List<Stack<char>> Stacks, List<Instruction> Instructions);
    public record Instruction(int Amount, int From, int To);
}
