using AoC2019.Days;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AoC2019.Tests
{
    public abstract class DayTestBase<TDay, TInput, TResult> where TDay : DayBase<TInput, TResult>, new()
    {
        protected TDay Day;
        protected TInput Input;

        protected abstract TResult ExpectedPart1Result { get; }
        protected abstract TResult ExpectedPart2Result { get; }

        [TestInitialize]
        public void Init()
        {
            Day = new TDay();
            Input = Day.ParseInput();
        }

        [TestCleanup]
        public void Cleanup()
        {
            Day = null;
            Input = default;
        }

        [TestMethod]
        public void TestPart1()
        {
            Assert.AreEqual(ExpectedPart1Result, Day.RunPart1(Input));
        }

        [TestMethod]
        public void TestPart2()
        {
            Assert.AreEqual(ExpectedPart2Result, Day.RunPart2(Input));
        }
    }
}
