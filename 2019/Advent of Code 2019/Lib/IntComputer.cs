using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using AoC2019.Extensions;

namespace AoC2019.Lib
{
    public sealed class IntComputer
    {
        private readonly DefaultDictionary<long, long> _memory = new DefaultDictionary<long, long>();
        private readonly Dictionary<int, Instruction> _instructions;
        private readonly BlockingCollection<long> _input;
        private readonly BlockingCollection<long> _output;

        private long _relativeBase;

        public long LastOutput { get; private set; }

        public IntComputer(IEnumerable<long> memory, BlockingCollection<long> input, BlockingCollection<long> output = null)
        {
            var i = 0L;
            foreach (var entry in memory)
            {
                _memory[i++] = entry;
            }

            _instructions = new Dictionary<int, Instruction>
            {
                {1, Add}, {2, Mul}, {3, Input}, {4, Output},
                {5, JumpIfTrue}, {6, JumpIfFalse}, {7, LessThan}, {8, Equals},
                {9, AdjustRelativeBase},
                {99, Halt}
            };
            _input = input;
            _output = output;
        }

        public long Run()
        {
            int ip;
            var postIp = 0;
            do
            {
                ip = postIp;
                var (opCode, a, b, c) = ParseInstruction(_memory[ip]);
                postIp = _instructions[opCode](ip, a, b, c);
            } while (ip != postIp);

            return LastOutput;
        }

        private int Add(int ip, int a, int b, int c)
        {
            Store(Load(_memory[ip + 1], a) + Load(_memory[ip + 2], b), _memory[ip + 3], c);
            return ip + 4;
        }

        private int Mul(int ip, int a, int b, int c)
        {
            Store(Load(_memory[ip + 1], a) * Load(_memory[ip + 2], b), _memory[ip + 3], c);
            return ip + 4;
        }

        private int Input(int ip, int a, int b, int c)
        {
            Store(_input.Take(), _memory[ip + 1], a);
            return ip + 2;
        }

        private int Output(int ip, int a, int b, int c)
        {
            LastOutput = Load(_memory[ip + 1], a);
            _output?.Add(LastOutput);
            Console.WriteLine(LastOutput);
            return ip + 2;
        }

        private int JumpIfTrue(int ip, int a, int b, int c)
        {
            if (Load(_memory[ip + 1], a) != 0)
                return (int) Load(_memory[ip + 2], b);

            return ip + 3;
        }

        private int JumpIfFalse(int ip, int a, int b, int c)
        {
            if (Load(_memory[ip + 1], a) == 0)
                return (int) Load(_memory[ip + 2], b);

            return ip + 3;
        }

        private int LessThan(int ip, int a, int b, int c)
        {
            Store(Load(_memory[ip + 1], a) < Load(_memory[ip + 2], b) ? 1 : 0, _memory[ip + 3], c);
            return ip + 4;
        }

        private int Equals(int ip, int a, int b, int c)
        {
            Store(Load(_memory[ip + 1], a) == Load(_memory[ip + 2], b) ? 1 : 0, _memory[ip + 3], c);
            return ip + 4;
        }

        private int AdjustRelativeBase(int ip, int a, int b, int c)
        {
            _relativeBase += (int) Load(_memory[ip + 1], a);
            return ip + 2;
        }

        private static int Halt(int ip, int a, int b, int c)
        {
            return ip;
        }

        private static (int, int, int, int) ParseInstruction(long instruction)
        {
            var opCode = (int)(instruction % 100);
            var mode = instruction / 100;
            var a = GetMode(mode, 0);
            var b = GetMode(mode, 1);
            var c = GetMode(mode, 2);
            return (opCode, a, b, c);
        }

        private long Load(long addressOrValue, int mode)
        {
            return mode switch
            {
                0 => _memory[(int) addressOrValue],
                1 => addressOrValue,
                2 => _memory[_relativeBase + addressOrValue],
                _ => throw new ArgumentOutOfRangeException(nameof(mode))
            };
        }

        private void Store(long value, long address, int mode)
        {
            _memory[(mode == 2 ? _relativeBase : 0) + address] = value;
        }

        private static int GetMode(long mode, int digit)
        {
            var e = Math.Pow(10, digit);
            return (int)(mode / e) % 10;
        }

        private delegate int Instruction(int ip, int a, int b, int c);
    }
}
