using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Tests
{
    [TestClass]
    public abstract class SolverTestsBase<TSolver, TPosition>
        where TSolver: ISolver
        where TPosition: IPosition
    {
        protected abstract TSolver CreateSolver();
        protected abstract TPosition CreatePosition();

        protected class TestSet 
        {
            public string Moves { get; set; }
            public int Score { get; set; }
        }

        protected IEnumerable<TestSet> ReadTestSets(string filename)
        {
            string[] lines = System.IO.File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                string[] tokens = line.Split(' ');
                string moves = tokens[0];
                int score = int.Parse(tokens[1]);
                yield return new TestSet
                {
                    Moves = moves,
                    Score = score
                };
            }    
        }

        [TestMethod]
        [DeploymentItem(@"TestSets\Test_L3_R1", "TestSets")]
        public void L3R1_Test()
        {
            var testSets = ReadTestSets(@"TestSets\Test_L3_R1");
            TestSets_Test(testSets);
        }

        [TestMethod]
        [DeploymentItem(@"TestSets\Test_L2_R1", "TestSets")]
        public void L2R1_Test()
        {
            var testSets = ReadTestSets(@"TestSets\Test_L2_R1");
            TestSets_Test(testSets);
        }

        [TestMethod]
        [DeploymentItem(@"TestSets\Test_L2_R2", "TestSets")]
        public void L2R2_Test()
        {
            var testSets = ReadTestSets(@"TestSets\Test_L2_R2");
            TestSets_Test(testSets);
        }

        private void TestSets_Test(IEnumerable<TestSet> testSets)
        {
            foreach (var testSet in testSets)
            {
                TPosition position = CreatePosition();
                int moves = position.Play(testSet.Moves.ToCharArray());
                // TODO: evaluate board and compare to TestSet.Score
                string display = position.ToString();

                TSolver solver = CreateSolver();
                int score = solver.Solve(position);
                Assert.AreEqual(testSet.Score, score);
            }
        }
    }
}
