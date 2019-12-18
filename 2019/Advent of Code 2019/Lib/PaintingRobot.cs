using System;
using System.Collections.Generic;
using System.Linq;
using AoC2019.Extensions;

namespace AoC2019.Lib
{
    public sealed class PaintingRobot : IntComputerAccessory
    {
        private readonly DefaultDictionary<(int X, int Y), int> _colors = new DefaultDictionary<(int X, int Y), int>();

        private (int Dx, int Dy) _direction = (0, -1);
        private (int X, int Y) _position = (0, 0);

        public PaintingRobot(IntComputer computer) : base(computer)
        {
            if (Computer.CurrentInput.FirstOrDefault() == 1)
                _colors[(0, 0)] = 1;
        }

        public (int X, int Y)[] WhitePanels => _colors.Where(e => e.Value == 1).Select(e => e.Key).ToArray();
        public int PaintedPanels => _colors.Count;

        public override IEnumerable<long> Run(params long[] input)
        {
            for (var i = 0; i < input.Length; i++)
            {
                var color = (int)input[i++];
                // ReSharper disable once RedundantCheckBeforeAssignment
                if (color != _colors[_position])
                    _colors[_position] = color;

                var turn = (int)input[i];
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

                _position = (_position.X + _direction.Dx, _position.Y + _direction.Dy);
                yield return _colors[_position];
            }
        }
    }
}
