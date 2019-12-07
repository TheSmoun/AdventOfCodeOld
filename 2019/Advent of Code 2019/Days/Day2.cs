using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2019.Days
{
    public sealed class Day2 : DayBase<int[], int>
    {
        public override string Name => "Day 2: 1202 Program Alarm";

        public override int[] ParseInput(IEnumerable<string> lines)
        {
            return lines.Single().Split(",").Select(int.Parse).ToArray();
        }

        public override int RunPart1(int[] input)
        {
            input[1] = 12;
            input[2] = 2;
            return RunIntComputer(input);
        }

        public override int RunPart2(int[] input)
        {
            for (var noun = 0; noun < 100; noun++)
            {
                for (var verb = 0; verb < 100; verb++)
                {
                    var memory = new List<int>(input) {[1] = noun, [2] = verb};
                    if (RunIntComputer(memory) == 19690720)
                        return 100 * noun + verb;
                }
            }

            throw new InvalidOperationException();
        }

        private static int RunIntComputer(IList<int> memory)
        {
            for (var i = 0; i < memory.Count; i += 4)
            {
                var opCode = memory[i];
                if (opCode == 1)
                    memory[memory[i + 3]] = memory[memory[i + 1]] + memory[memory[i + 2]];
                else if (opCode == 2)
                    memory[memory[i + 3]] = memory[memory[i + 1]] * memory[memory[i + 2]];
                else if (opCode == 99)
                    break;
                else
                    throw new InvalidOperationException();
            }

            return memory[0];
        }
    }
}
