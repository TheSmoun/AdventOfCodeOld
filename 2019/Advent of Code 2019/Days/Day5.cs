using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2019.Days
{
    public sealed class Day5 : DayBase<int[], int>
    {
        protected override string Name => "Day 5: Sunny with a Chance of Asteroids";

        private int _input;
        private int _lastOutput;

        protected override int[] ParseInput(IEnumerable<string> lines)
        {
            return lines.Single().Split(",").Select(int.Parse).ToArray();
        }

        protected override int RunPart1(int[] input)
        {
            _input = 1;
            return RunIntComputer(input);
        }

        protected override int RunPart2(int[] input)
        {
            _input = 5;
            return RunIntComputer(input);
        }

        private int RunIntComputer(int[] memory)
        {
            var instructions = new Dictionary<int, Instruction>
            {
                {1, Add}, {2, Mul}, {3, Input}, {4, Output},
                {5, JumpIfTrue}, {6, JumpIfFalse}, {7, LessThan}, {8, Equals},
                {99, Halt}
            };

            int ip;
            var postIp = 0;
            do
            {
                ip = postIp;
                var (opCode, a, b) = ParseInstruction(memory[ip]);
                postIp = instructions[opCode](ref memory, ip, a, b);
            } while (ip != postIp);
            
            return _lastOutput;
        }

        private static int Add(ref int[] memory, int ip, int a, int b)
        {
            memory[memory[ip + 3]] = GetValue(memory, memory[ip + 1], a) + GetValue(memory, memory[ip + 2], b);
            return ip + 4;
        }

        private static int Mul(ref int[] memory, int ip, int a, int b)
        {
            memory[memory[ip + 3]] = GetValue(memory, memory[ip + 1], a) * GetValue(memory, memory[ip + 2], b);
            return ip + 4;
        }

        private int Input(ref int[] memory, int ip, int a, int b)
        {
            memory[memory[ip + 1]] = _input;
            return ip + 2;
        }

        private int Output(ref int[] memory, int ip, int a, int b)
        {
            _lastOutput = GetValue(memory, memory[ip + 1], a);
            Console.WriteLine($"   -> {_lastOutput} ({ip})");
            return ip + 2;
        }

        private static int JumpIfTrue(ref int[] memory, int ip, int a, int b)
        {
            if (GetValue(memory, memory[ip + 1], a) != 0)
                return GetValue(memory, memory[ip + 2], b);
            
            return ip + 3;
        }

        private static int JumpIfFalse(ref int[] memory, int ip, int a, int b)
        {
            if (GetValue(memory, memory[ip + 1], a) == 0)
                return GetValue(memory, memory[ip + 2], b);

            return ip + 3;
        }

        private static int LessThan(ref int[] memory, int ip, int a, int b)
        {
            memory[memory[ip + 3]] = GetValue(memory, memory[ip + 1], a) < GetValue(memory, memory[ip + 2], b) ? 1 : 0;
            return ip + 4;
        }

        private static int Equals(ref int[] memory, int ip, int a, int b)
        {
            memory[memory[ip + 3]] = GetValue(memory, memory[ip + 1], a) == GetValue(memory, memory[ip + 2], b) ? 1 : 0;
            return ip + 4;
        }

        private static int Halt(ref int[] memory, int ip, int a, int b)
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
            return (int) (mode / e) % 10;
        }

        private delegate int Instruction(ref int[] memory, int ip, int a, int b);
    }
}
