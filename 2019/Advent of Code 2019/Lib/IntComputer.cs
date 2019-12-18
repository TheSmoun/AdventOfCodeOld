using System;
using System.Collections.Generic;
using AoC2019.Extensions;

namespace AoC2019.Lib
{
    public sealed class IntComputer : IntCodeRunnable
    {
        private readonly DefaultDictionary<long, long> _memory = new DefaultDictionary<long, long>();
        private readonly Dictionary<int, Instruction> _instructions;

        private long _relativeBase;
        private int _ip;

        public bool Halted { get; private set; }

        public IntComputer(IEnumerable<long> memory, params long[] input) : base(input)
        {
            var i = 0L;
            foreach (var entry in memory)
            {
                _memory[i++] = entry;
            }

            _instructions = new Dictionary<int, Instruction>
            {
                {1, Add}, {2, Mul}, {3, InputInstruction}, {4, Output},
                {5, JumpIfTrue}, {6, JumpIfFalse}, {7, LessThan}, {8, Equals},
                {9, AdjustRelativeBase},
                {99, Halt}
            };
        }

        public override IEnumerable<long> Run(params long[] input)
        {
            if (Halted)
                throw new InvalidOperationException("cannot start a halted computer");

            Input.EnqueueAll(input);
            return Run();
        }

        public IEnumerable<long> Run()
        {
            if (Halted)
                throw new InvalidOperationException("cannot start a halted computer");

            bool? halt;
            do
            {
                var (opCode, modeA, modeB, modeC) = ParseInstruction(_memory[_ip]);
                long? output;
                (_ip, output, halt) = _instructions[opCode](modeA, modeB, modeC);
                Halted = halt.GetValueOrDefault();
                if (output.HasValue) yield return output.Value;
            } while (!halt.HasValue);
        }

        private (int PostIp, long? Output, bool? Halt) Add(int modeA, int modeB, int modeC)
        {
            Store(Load(_memory[_ip + 1], modeA) + Load(_memory[_ip + 2], modeB), _memory[_ip + 3], modeC);
            return (_ip + 4, null, null);
        }

        private (int PostIp, long? Output, bool? Halt) Mul(int modeA, int modeB, int modeC)
        {
            Store(Load(_memory[_ip + 1], modeA) * Load(_memory[_ip + 2], modeB), _memory[_ip + 3], modeC);
            return (_ip + 4, null, null);
        }

        private (int PostIp, long? Output, bool? Halt) InputInstruction(int modeA, int modeB, int modeC)
        {
            if (Input.Count == 0)
                return (_ip, null, false);

            Store(Input.Dequeue(), _memory[_ip + 1], modeA);
            return (_ip + 2, null, null);
        }

        private (int PostIp, long? Output, bool? Halt) Output(int modeA, int modeB, int modeC)
        {
            return (_ip + 2, Load(_memory[_ip + 1], modeA), null);
        }

        private (int PostIp, long? Output, bool? Halt) JumpIfTrue(int modeA, int modeB, int modeC)
        {
            if (Load(_memory[_ip + 1], modeA) != 0)
                return ((int) Load(_memory[_ip + 2], modeB), null, null);

            return (_ip + 3, null, null);
        }

        private (int PostIp, long? Output, bool? Halt) JumpIfFalse(int modeA, int modeB, int modeC)
        {
            if (Load(_memory[_ip + 1], modeA) == 0)
                return ((int) Load(_memory[_ip + 2], modeB), null, null);

            return (_ip + 3, null, null);
        }

        private (int PostIp, long? Output, bool? Halt) LessThan(int modeA, int modeB, int modeC)
        {
            Store(Load(_memory[_ip + 1], modeA) < Load(_memory[_ip + 2], modeB) ? 1 : 0, _memory[_ip + 3], modeC);
            return (_ip + 4, null, null);
        }

        private (int PostIp, long? Output, bool? Halt) Equals(int modeA, int modeB, int modeC)
        {
            Store(Load(_memory[_ip + 1], modeA) == Load(_memory[_ip + 2], modeB) ? 1 : 0, _memory[_ip + 3], modeC);
            return (_ip + 4, null, null);
        }

        private (int PostIp, long? Output, bool? Halt) AdjustRelativeBase(int modeA, int modeB, int modeC)
        {
            _relativeBase += (int) Load(_memory[_ip + 1], modeA);
            return (_ip + 2, null, null);
        }

        private (int PostIp, long? Output, bool? Halt) Halt(int modeA, int modeB, int modeC)
        {
            return (_ip, null, true);
        }

        private static (int, int, int, int) ParseInstruction(long instruction)
        {
            var opCode = (int)(instruction % 100);
            var mode = instruction / 100;
            var modeA = GetMode(mode, 0);
            var modeB = GetMode(mode, 1);
            var modeC = GetMode(mode, 2);
            return (opCode, modeA, modeB, modeC);
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

        private delegate (int PostIp, long? Output, bool? Halt) Instruction(int modeA, int modeB, int modeC);
    }
}
