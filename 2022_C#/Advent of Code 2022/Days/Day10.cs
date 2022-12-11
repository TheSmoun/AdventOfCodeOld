using System.Diagnostics;

namespace Advent_of_Code_2022.Days;

public class Day10 : DayBase<List<Day10.Instruction>, string>
{
    public override string Name => "Day 10: Cathode-Ray Tube";
    
    public override List<Instruction> ParseInput(IEnumerable<string> lines)
    {
        return lines.Select(l =>
        {
            Instruction i = l[..4] switch
            {
                "noop" => new NoopInstruction(),
                "addx" => new AddInstruction(int.Parse(l[5..])),
                _ => throw new UnreachableException()
            };
            return i;
        }).ToList();
    }

    public override string RunPart1(List<Instruction> input)
    {
        var cycles = new HashSet<int> { 20, 60, 100, 140, 180, 220 };
        var state = new State(0, 1);
        var lastState = state;
        var result = 0;
        
        foreach (var instruction in input)
        {
            foreach (var newState in instruction.Tick(state))
            {
                if (cycles.Contains(newState.Cycle))
                {
                    result += newState.Cycle * lastState.Register;
                }
                
                lastState = newState;
            }

            state = lastState;
        }

        return result.ToString();
    }

    public override string RunPart2(List<Instruction> input)
    {
        var drawing = new List<char>();
        var state = new State(0, 1);
        var lastState = state;
        
        foreach (var instruction in input)
        {
            foreach (var newState in instruction.Tick(state))
            {
                drawing.Add(Math.Abs(lastState.Cycle % 40 - lastState.Register) <= 1 ? '#' : '.');
                lastState = newState;
            }

            state = lastState;
        }

        foreach (var line in drawing.Chunk(40))
        {
            Console.WriteLine(string.Join(string.Empty, line));
        }
        
        return string.Empty;
    }

    public abstract class Instruction
    {
        public abstract IEnumerable<State> Tick(State state);
    }
    
    public class NoopInstruction : Instruction
    {
        public override IEnumerable<State> Tick(State state)
        {
            yield return state with { Cycle = state.Cycle + 1 };
        }
    }
    
    public class AddInstruction : Instruction
    {
        private readonly int _amount;

        public AddInstruction(int amount)
        {
            _amount = amount;
        }
        
        public override IEnumerable<State> Tick(State state)
        {
            yield return state with { Cycle = state.Cycle + 1 };
            yield return new State(state.Cycle + 2, state.Register + _amount);
        }
    }

    public record State(int Cycle, int Register);
}
