using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AoC2019.Lib
{
    public sealed class IntComputer
    {
        private readonly int[] _memory;
        private readonly Dictionary<int, Instruction> _instructions;
        private readonly Queue<int> _input;
        private readonly Queue<int> _output;

        public int LastOutput { get; private set; }

        public IntComputer(IEnumerable<int> memory, Queue<int> input, Queue<int> output = null)
        {
            _memory = memory.ToArray();
            _instructions = new Dictionary<int, Instruction>
            {
                {1, Add}, {2, Mul}, {3, Input}, {4, Output},
                {5, JumpIfTrue}, {6, JumpIfFalse}, {7, LessThan}, {8, Equals},
                {99, Halt}
            };
            _input = input;
            _output = output;
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

            return LastOutput;
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
            while (_input.Count == 0)
            {
                Thread.Sleep(1);
            }

            _memory[_memory[ip + 1]] = _input.Dequeue();
            return ip + 2;
        }

        private int Output(int ip, int a, int b)
        {
            LastOutput = GetValue(_memory, _memory[ip + 1], a);
            _output?.Enqueue(LastOutput);
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
