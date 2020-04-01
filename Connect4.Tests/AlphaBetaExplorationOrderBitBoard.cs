using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Connect4.Tests
{
    [TestClass]
    public class AlphaBetaExplorationOrderBitBoard : SolverTestsBase<AlphaBetaExplorationOrder, BitBoard>
    {
        protected override AlphaBetaExplorationOrder CreateSolver() => new AlphaBetaExplorationOrder();

        protected override BitBoard CreatePosition() => new BitBoard();
    }
}
