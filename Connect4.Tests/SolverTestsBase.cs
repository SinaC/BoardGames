using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Tests
{
    public abstract class SolverTestsBase
    {
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
    }
}
