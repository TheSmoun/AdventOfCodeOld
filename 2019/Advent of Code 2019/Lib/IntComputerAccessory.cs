using System.Collections.Concurrent;

namespace AoC2019.Lib
{
    public abstract class IntComputerAccessory
    {
        protected readonly IntComputer Computer;
        protected readonly BlockingCollection<long> Input;
        protected readonly BlockingCollection<long> Output;

        protected IntComputerAccessory(IntComputer computer, BlockingCollection<long> input, BlockingCollection<long> output)
        {
            Computer = computer;
            Input = input;
            Output = output;
        }

        public void Run()
        {
            while (RunLoop)
            {
                Loop();
            }
        }

        protected bool RunLoop
        {
            get
            {
                if (!Computer.Running)
                    return false;

                lock (Input)
                {
                    return !Input.IsCompleted;
                }
            }
        }

        protected abstract void Loop();
    }
}
