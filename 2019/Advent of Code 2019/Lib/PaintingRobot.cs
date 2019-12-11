using System;
using System.Collections.Concurrent;
using System.Linq;
using AoC2019.Extensions;

namespace AoC2019.Lib
{
    public sealed class PaintingRobot
    {
        private readonly DefaultDictionary<(int X, int Y), int> _colors = new DefaultDictionary<(int X, int Y), int>();
        private readonly Func<bool> _isComputerRunning;
        private readonly BlockingCollection<long> _input;
        private readonly BlockingCollection<long> _output;

        private (int Dx, int Dy) _direction = (0, -1);
        private (int X, int Y) _position = (0, 0);

        public PaintingRobot(Func<bool> isComputerRunning, BlockingCollection<long> input, BlockingCollection<long> output)
        {
            _isComputerRunning = isComputerRunning;
            _input = input;
            _output = output;

            var firstColor = _output.Take();
            if (firstColor == 1)
                _colors[(0, 0)] = 1;
            _output.Add(firstColor);
        }

        public (int X, int Y)[] WhitePanels => _colors.Where(e => e.Value == 1).Select(e => e.Key).ToArray();
        public int PaintedPanels => _colors.Count;

        public void Run()
        {
            while (_isComputerRunning())
            {
                ConsoleEx.WriteColoredLine("Robot: Loop", ConsoleColor.Yellow);
                var color = (int) _input.Take();
                // ReSharper disable once RedundantCheckBeforeAssignment
                if (color != _colors[_position])
                    _colors[_position] = color;

                ConsoleEx.WriteColoredLine("Robot: Color", ConsoleColor.Yellow);

                var turn = (int) _input.Take();
                if (turn == 0)
                {
                    _direction = _direction switch
                    {
                        (0, -1) => (-1, 0),
                        (-1, 0) => (0, 1),
                        (0, 1) => (1, 0),
                        (1, 0) => (0, -1),
                        _ => throw new InvalidOperationException()
                    };
                }
                else
                {
                    _direction = _direction switch
                    {
                        (0, -1) => (1, 0),
                        (-1, 0) => (0, -1),
                        (0, 1) => (-1, 0),
                        (1, 0) => (0, 1),
                        _ => throw new InvalidOperationException()
                    };
                }

                ConsoleEx.WriteColoredLine($"Robot: Turn {turn} ({_direction.Dx}, {_direction.Dy})", ConsoleColor.Yellow);

                _position = (_position.X + _direction.Dx, _position.Y + _direction.Dy);
                _output.Add(_colors[_position]);

                ConsoleEx.WriteColoredLine($"Robot: Output {_position} {_colors[_position]}", ConsoleColor.Yellow);
            }
        }
    }
}
