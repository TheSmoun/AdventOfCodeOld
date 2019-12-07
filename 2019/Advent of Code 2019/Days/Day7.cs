using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace AoC2019.Days
{
    public sealed class Day7 : DayBase<int[], int>
    {
        protected override string Name => "Day 7: Amplification Circuit";

        protected override int[] ParseInput(IEnumerable<string> lines)
        {
            return lines.Single().Split(",").Select(int.Parse).ToArray();
        }

        protected override int RunPart1(int[] input)
        {
            return Enumerable.Range(0, 5).Permutations()
                .Select(p => p.Aggregate(0, (current, c) => new Amplifier(input, c, current).Run()))
                .Max();
        }

        protected override int RunPart2(int[] input)
        {
            return 0;
        }

        private class Amplifier
        {
            private readonly int[] _memory;
            private readonly Dictionary<int, Instruction> _instructions;
            private readonly int _mode;
            private readonly int _input;

            private bool _modeRead;
            private int _output;

            public Amplifier(IEnumerable<int> memory, int mode, int input)
            {
                _memory = memory.ToArray();
                _instructions = new Dictionary<int, Instruction>
                {
                    {1, Add}, {2, Mul}, {3, Input}, {4, Output},
                    {5, JumpIfTrue}, {6, JumpIfFalse}, {7, LessThan}, {8, Equals},
                    {99, Halt}
                };
                _mode = mode;
                _input = input;
            }

            public int Run()
            {
                int ip;
                var postIp = 0;
                do
                {
                    ip = postIp;
                    var (opCode, a, b) = ParseInstruction(_memory[ip]);
                    postIp = _instructions[opCode](ip, a, b);
                } while (ip != postIp);

                return _output;
            }

            private int Add(int ip, int a, int b)
            {
                _memory[_memory[ip + 3]] = GetValue(_memory, _memory[ip + 1], a) + GetValue(_memory, _memory[ip + 2], b);
                return ip + 4;
            }

            private int Mul(int ip, int a, int b)
            {
                _memory[_memory[ip + 3]] = GetValue(_memory, _memory[ip + 1], a) * GetValue(_memory, _memory[ip + 2], b);
                return ip + 4;
            }

            private int Input(int ip, int a, int b)
            {
                int input;
                if (_modeRead)
                {
                    input = _input;
                }
                else
                {
                    input = _mode;
                    _modeRead = true;
                }

                _memory[_memory[ip + 1]] = input;
                return ip + 2;
            }

            private int Output(int ip, int a, int b)
            {
                _output = GetValue(_memory, _memory[ip + 1], a);
                return ip + 2;
            }

            private int JumpIfTrue(int ip, int a, int b)
            {
                if (GetValue(_memory, _memory[ip + 1], a) != 0)
                    return GetValue(_memory, _memory[ip + 2], b);

                return ip + 3;
            }

            private int JumpIfFalse(int ip, int a, int b)
            {
                if (GetValue(_memory, _memory[ip + 1], a) == 0)
                    return GetValue(_memory, _memory[ip + 2], b);

                return ip + 3;
            }

            private int LessThan(int ip, int a, int b)
            {
                _memory[_memory[ip + 3]] = GetValue(_memory, _memory[ip + 1], a) < GetValue(_memory, _memory[ip + 2], b) ? 1 : 0;
                return ip + 4;
            }

            private int Equals(int ip, int a, int b)
            {
                _memory[_memory[ip + 3]] = GetValue(_memory, _memory[ip + 1], a) == GetValue(_memory, _memory[ip + 2], b) ? 1 : 0;
                return ip + 4;
            }

            private static int Halt(int ip, int a, int b)
            {
                return ip;
            }

            private static (int, int, int) ParseInstruction(int instruction)
            {
                var opCode = instruction % 100;
                var mode = instruction / 100;
                var a = GetMode(mode, 0);
                var b = GetMode(mode, 1);
                return (opCode, a, b);
            }

            private static int GetValue(IReadOnlyList<int> memory, int addressOrValue, int mode)
            {
                return mode switch
                {
                    0 => memory[addressOrValue],
                    1 => addressOrValue,
                    _ => throw new ArgumentOutOfRangeException(nameof(mode))
                };
            }

            private static int GetMode(int mode, int digit)
            {
                var e = Math.Pow(10, digit);
                return (int)(mode / e) % 10;
            }

            private delegate int Instruction(int ip, int a, int b);
        }
    }
}
