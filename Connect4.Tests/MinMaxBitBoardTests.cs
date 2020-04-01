using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Connect4.Tests
{
    [TestClass]
    public class MinMaxBitBoardTests : SolverTestsBase<MinMax, BitBoard>
    {
        protected override MinMax CreateSolver() => new MinMax();

        protected override BitBoard CreatePosition() => new BitBoard();
    }
}
