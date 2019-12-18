using System.Collections.Generic;
using System.Linq;
using AoC2019.Extensions;

namespace AoC2019.Lib
{
    public sealed class ArcadeCabinet : IntComputerAccessory
    {
        public int Score { get; private set; }

        public int Blocks => _tiles.Values.Count(t => t == 2);
        public int Paddle => FindTile(3);
        public int Ball => FindTile(4);

        private readonly Dictionary<(int X, int Y), int> _tiles = new Dictionary<(int X, int Y), int>();

        public ArcadeCabinet(IntComputer computer) : base(computer) { }

        public override IEnumerable<long> Run(params long[] input)
        {
            var queue = input.ToQueue();
            while (queue.Count > 0)
            {
                var (x, y, tileId) = ((int)queue.Dequeue(), (int)queue.Dequeue(), (int)queue.Dequeue());
                if ((x, y) == (-1, 0))
                {
                    Score = tileId;
                }
                else
                {
                    _tiles[(x, y)] = tileId;
                }
            }

            yield return Ball.CompareTo(Paddle);
        }

        private int FindTile(int tile)
        {
            if (_tiles.Values.All(t => t != tile))
                return -1;

            return _tiles.First(t => t.Value == tile).Key.X;
        }
    }
}
