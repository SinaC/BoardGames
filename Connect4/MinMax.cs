using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public class MinMax : ISolver
    {
        public int Solve(IPosition position)
        {
            return Negamax(position);
        }
        
        private int Negamax(IPosition position)
        {
            if (position.NbMoves == position.Width * position.Height) // check for draw game
                return 0;

            for (int x = 0; x < position.Width; x++) // check if current player can win next move
                if (position.CanPlay(x) && position.IsWinningMove(x))
                    return (position.Width * position.Height + 1 - position.NbMoves) / 2;

            int bestScore = -position.Width * position.Height; // init the best possible score with a lower bound of score.

            for (int x = 0; x < position.Width; x++) // compute the score of all possible next move and keep the best one
                if (position.CanPlay(x))
                {
                    IPosition newPosition = position.Clone();
                    newPosition.Play(x);               // It's opponent turn in P2 position after current player plays x column.
                    int score = -Negamax(newPosition); // If current player plays col x, his score will be the opposite of opponent's score after playing col x
                    if (score > bestScore)
                        bestScore = score; // keep track of best possible score so far.
                }

            return bestScore;
        }
    }
}
