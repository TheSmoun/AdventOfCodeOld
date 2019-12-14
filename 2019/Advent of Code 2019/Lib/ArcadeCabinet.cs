using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace AoC2019.Lib
{
    public sealed class ArcadeCabinet : IntComputerAccessory
    {
        public int Score { get; private set; }

        public int Blocks => _tiles.Values.Count(t => t == 2);
        public int Paddle => FindTile(3);
        public int Ball => FindTile(4);

        private readonly Dictionary<(int X, int Y), int> _tiles = new Dictionary<(int X, int Y), int>();

        public ArcadeCabinet(IntComputer computer, BlockingCollection<long> input, BlockingCollection<long> output) : base(computer, input, output) { }

        protected override void Loop()
        {
            while (RunInnerLoop)
            {
                lock (Input)
                {
                    var (x, y, tileId) = ((int)Input.Take(), (int)Input.Take(), (int)Input.Take());

                    if ((x, y) == (-1, 0))
                    {
                        Score = tileId;
                    }
                    else
                    {
                        _tiles[(x, y)] = tileId;
                    }
                }
            }

            lock (Output)
            {
                Output.Add(Ball.CompareTo(Paddle));
            }
        }

        private bool RunInnerLoop
        {
            get
            {
                lock (Input)
                {
                    return RunLoop && Input.Count > 3;
                }
            }
        }

        private int FindTile(int tile)
        {
            if (_tiles.Values.All(t => t != tile))
                return -1;

            return _tiles.First(t => t.Value == tile).Key.X;
        }
    }
}
