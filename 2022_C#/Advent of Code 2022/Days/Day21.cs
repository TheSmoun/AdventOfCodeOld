using System.Diagnostics;
using System.Text.RegularExpressions;
using Advent_of_Code_2022.Extensions;

namespace Advent_of_Code_2022.Days;

public partial class Day21 : DayBase<Dictionary<string, Day21.MonkeyOperation>, long>
{
    private static readonly Regex Regex = ParsingRegex();
    
    public override string Name => "Day 21: Monkey Math";
    
    public override Dictionary<string, MonkeyOperation> ParseInput(IEnumerable<string> lines)
    {
        var operations = lines
            .Select<string, MonkeyOperation>(l =>
            {
                var match = Regex.Match(l);
                var name = match.GetValue("name");
                if (match.Groups["number"].Success)
                {
                    var number = match.GetIntValue("number");
                    return new NumberOperation
                    {
                        Name = name,
                        Number = number
                    };
                }

                var leftName = match.GetValue("left");
                var rightName = match.GetValue("right");
                var operation = match.GetValue("op");
                return operation switch
                {
                    "+" => InitMathOperation<AddOperation>(name, leftName, rightName),
                    "-" => InitMathOperation<SubOperation>(name, leftName, rightName),
                    "*" => InitMathOperation<MulOperation>(name, leftName, rightName),
                    "/" => InitMathOperation<DivOperation>(name, leftName, rightName),
                    _ => throw new UnreachableException()
                };
            })
            .ToDictionary(m => m.Name, m => m);

        foreach (var operation in operations.Values.OfType<MathOperation>())
        {
            operation.Left = operations[operation.LeftName];
            operation.Right = operations[operation.RightName];
        }

        return operations;
    }

    public override long RunPart1(Dictionary<string, MonkeyOperation> input)
        => input["root"].Compute();

    public override long RunPart2(Dictionary<string, MonkeyOperation> input)
    {
        var human = (NumberOperation) input["humn"];
        var root = input["root"];
        
        var humanNumber = human.Number;
        human.Number = 0;

        var current = root;
        while (current != human)
        {
            if (current is not MathOperation currentMath)
                throw new UnreachableException();

            var left = currentMath.Left;
            var right = currentMath.Right;

            var beforeLeft = left.Compute();
            var beforeRight = right.Compute();
            
            human.Number = humanNumber;

            var afterLeft = left.Compute();
            var afterRight = right.Compute();

            if (beforeLeft != afterLeft)
            {
                if (current == root)
                {
                    humanNumber = beforeRight;
                }
                else
                {
                    humanNumber = current switch
                    {
                        AddOperation => humanNumber - beforeRight,
                        SubOperation => humanNumber + beforeRight,
                        MulOperation => humanNumber / beforeRight,
                        DivOperation => humanNumber * beforeRight,
                        _ => throw new UnreachableException()
                    };
                }

                current = currentMath.Left;
            }
            else if (beforeRight != afterRight)
            {
                if (current == root)
                {
                    humanNumber = beforeLeft;
                }
                else
                {
                    humanNumber = current switch
                    {
                        AddOperation => humanNumber - beforeLeft,
                        SubOperation => beforeLeft - humanNumber,
                        MulOperation => humanNumber / beforeLeft,
                        DivOperation => beforeLeft / humanNumber,
                        _ => throw new UnreachableException()
                    };
                }

                current = currentMath.Right;
            }
            else
            {
                throw new UnreachableException();
            }
        }
        
        return humanNumber;
    }

    private static MathOperation InitMathOperation<T>(string name, string left, string right)
        where T : MathOperation, new()
    {
        return new T
        {
            Name = name,
            LeftName = left,
            RightName = right
        };
    }

    public abstract class MonkeyOperation
    {
        public string Name { get; init; } = default!;

        public abstract long Compute();
    }

    public class NumberOperation : MonkeyOperation
    {
        public long Number { get; set; }

        public override long Compute() => Number;
    }

    public abstract class MathOperation : MonkeyOperation
    {
        public string LeftName { get; init; } = default!;
        public string RightName { get; init; } = default!;
        
        public MonkeyOperation Left { get; set; } = default!;
        public MonkeyOperation Right { get; set; } = default!;
    }

    public class AddOperation : MathOperation
    {
        public override long Compute() => Left.Compute() + Right.Compute();
    }

    public class SubOperation : MathOperation
    {
        public override long Compute() => Left.Compute() - Right.Compute();
    }

    public class MulOperation : MathOperation
    {
        public override long Compute() => Left.Compute() * Right.Compute();
    }

    public class DivOperation : MathOperation
    {
        public override long Compute() => Left.Compute() / Right.Compute();
    }

    [GeneratedRegex(@"^(?<name>.{4}): ((?<number>\d+)|(?<left>.{4}) (?<op>[+\-*/]{1}) (?<right>.{4}))$")]
    private static partial Regex ParsingRegex();
}
