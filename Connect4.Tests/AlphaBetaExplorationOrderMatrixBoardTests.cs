using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Connect4.Tests
{
    [TestClass]
    public class AlphaBetaExplorationOrderMatrixBoardTests : SolverTestsBase<AlphaBetaExplorationOrder, MatrixBoard>
    {
        protected override AlphaBetaExplorationOrder CreateSolver() => new AlphaBetaExplorationOrder();

        protected override MatrixBoard CreatePosition() => new MatrixBoard();
    }
}
