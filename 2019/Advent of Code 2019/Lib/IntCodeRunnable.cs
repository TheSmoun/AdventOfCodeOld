using System.Collections.Generic;
using AoC2019.Extensions;

namespace AoC2019.Lib
{
    public abstract class IntCodeRunnable
    {
        protected readonly Queue<long> Input = new Queue<long>();

        public IEnumerable<long> CurrentInput => Input;

        protected IntCodeRunnable(params long[] input)
        {
            Input.EnqueueAll(input);
        }

        public abstract IEnumerable<long> Run(params long[] input);
    }
}
