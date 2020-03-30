using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Connect4.Tests
{
    [TestClass]
    public class MinMaxMatrixBoardTests : SolverTestsBase<MinMax, MatrixBoard>
    {
        protected override MinMax CreateSolver() => new MinMax();

        protected override MatrixBoard CreatePosition() => new MatrixBoard();
    }
}
