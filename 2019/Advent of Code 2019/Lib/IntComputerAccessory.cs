using System.Linq;

namespace AoC2019.Lib
{
    public abstract class IntComputerAccessory : IntCodeRunnable
    {
        protected readonly IntComputer Computer;

        protected IntComputerAccessory(IntComputer computer, params long[] input) : base(input)
        {
            Computer = computer;
        }

        public void Run()
        {
            var input = new long[0];
            while (!Computer.Halted)
            {
                input = Run(Computer.Run(input).ToArray()).ToArray();
            }
        }
    }
}
