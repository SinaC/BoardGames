using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Connect4.Tests
{
    [TestClass]
    public class MinMaxTests : SolverTestsBase
    {
        [TestMethod]
        [DeploymentItem(@"TestSets\Test_L3_R1", "TestSets")]
        public void L3R1_Test()
        {
            var testSets = ReadTestSets(@"TestSets\Test_L3_R1");
            foreach (var testSet in testSets)
            {
                MatrixBoard matrixBoard = new MatrixBoard();
                int moves = matrixBoard.Play(testSet.Moves.ToCharArray());
                // TODO: evaluate board and compare to TestSet.Score
                string display = matrixBoard.ToString();
            }
        }
    }
}
