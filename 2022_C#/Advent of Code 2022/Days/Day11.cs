using Advent_of_Code_2022.Extensions;
using MoreLinq;

namespace Advent_of_Code_2022.Days;

public class Day11 : DayBase<List<Day11.Monkey>, long>
{
    public override string Name => "Day 11: Monkey in the Middle";
    
    public override List<Monkey> ParseInput(IEnumerable<string> lines)
    {
        var monkeysInput = lines.Split(string.Empty).ToList();
        var monkeyLinks = new Dictionary<Monkey, (int, int)>();
        var monkeys = monkeysInput.Select(i =>
        {
            var parts = i.ToList();
            var id = int.Parse(parts[0].Substring(7, 1));
            var items = parts[1][18..].Split(',').Select(s => long.Parse(s.Trim()));

            Operation operation;
            var operationString = parts[2][19..]!;
            if (operationString == "old * old")
                operation = new SquareOperation();
            else if (operationString.StartsWith("old + "))
                operation = new AddOperation(int.Parse(operationString[6..]));
            else
                operation = new MulOperation(int.Parse(operationString[6..]));

            var test = int.Parse(parts[3][21..]);
            var monkey = new Monkey(id, items, operation, test);

            var trueMonkey = int.Parse(parts[4][28..]);
            var falseMonkey = int.Parse(parts[5][29..]);
            monkeyLinks[monkey] = (trueMonkey, falseMonkey);

            return monkey;
        }).ToList();

        foreach (var monkey in monkeys)
        {
            monkey.TrueMonkey = monkeys.First(m => m.Id == monkeyLinks[monkey].Item1);
            monkey.FalseMonkey = monkeys.First(m => m.Id == monkeyLinks[monkey].Item2);
        }
        
        return monkeys;
    }

    public override long RunPart1(List<Monkey> input)
        => PlayGame(input, 20, i => i / 3);

    public override long RunPart2(List<Monkey> input)
    {
        var lcm = input.Select(i => i.Test).Lcm();
        return PlayGame(input, 10000, i => i % lcm);
    }

    private static long PlayGame(List<Monkey> monkeys, int rounds, StressDownFunc stressDown)
    {
        foreach (var monkey in monkeys)
        {
            monkey.StressDown = stressDown;
        }
        
        for (var i = 0; i < rounds; i++)
        {
            PlayRound(monkeys);
        }

        return monkeys.Select(m => m.InspectedItems).OrderDescending().Take(2).Product();
    }

    private static void PlayRound(List<Monkey> monkeys)
    {
        foreach (var monkey in monkeys)
        {
            monkey.PlayTurn();
        }
    }

    public class Monkey
    {
        public int Id { get; }
        
        public Monkey TrueMonkey { get; set; } = default!;
        public Monkey FalseMonkey { get; set; } = default!;
        public StressDownFunc StressDown { get; set; } = default!;
        public long InspectedItems { get; private set; }
        public long Test { get; }

        private readonly List<long> _items;
        private readonly Operation _operation;

        public Monkey(int id, IEnumerable<long> items, Operation operation, long test)
        {
            Id = id;
            _items = new List<long>(items);
            _operation = operation;
            Test = test;
        }

        public void PlayTurn()
        {
            foreach (var item in _items)
            {
                HandleItem(item);
            }
            
            _items.Clear();
        }

        private void HandleItem(long item)
        {
            InspectedItems++;
            item = StressDown(_operation.Compute(item));
            
            if (item % Test == 0)
                TrueMonkey._items.Add(item);
            else
                FalseMonkey._items.Add(item);
        }

        public override string ToString()
        {
            return $"[{Id}] - Inspected Items: {InspectedItems}";
        }
    }
    
    public abstract class Operation
    {
        public abstract long Compute(long item);
    }

    public class AddOperation : Operation
    {
        private readonly long _amount;

        public AddOperation(long amount)
        {
            _amount = amount;
        }
        
        public override long Compute(long item) => item + _amount;
    }
    
    public class MulOperation : Operation
    {
        private readonly long _amount;

        public MulOperation(long amount)
        {
            _amount = amount;
        }

        public override long Compute(long item) => item * _amount;
    }

    public class SquareOperation : Operation
    {
        public override long Compute(long item) => item * item;
    }

    public delegate long StressDownFunc(long item);
}
