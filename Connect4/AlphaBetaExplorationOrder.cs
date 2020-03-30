using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public class AlphaBetaExplorationOrder : ISolver
    {
        private int[] columnOrder; // column exploration order

        public int Solve(IPosition position)
        {
            columnOrder = new int[position.Width];
            for (int i = 0; i < position.Width; i++)
                columnOrder[i] = position.Width / 2 + (1 - 2 * (i % 2)) * (i + 1) / 2; // initialize the column exploration order, starting with center columns

            return Negamax(position, -position.Width * position.Height / 2, position.Width * position.Height / 2);
        }

        public int Negamax(IPosition position, int alpha, int beta)
        {
            if (position.NbMoves == position.Width * position.Height) // check for draw game
                return 0;

            for (int x = 0; x < position.Width; x++) // check if current player can win next move
                if (position.CanPlay(x) && position.IsWinningMove(x))
                    return (position.Width * position.Height + 1 - position.NbMoves) / 2;

            int max = (position.Width * position.Height - 1 - position.NbMoves) / 2;	// upper bound of our score as we cannot win immediately

            if (beta > max)
            {
                beta = max;                     // there is no need to keep beta above our max possible score.
                if (alpha >= beta)
                    return beta;  // prune the exploration if the [alpha;beta] window is empty.
            }

            for (int x = 0; x < position.Width; x++) // compute the score of all possible next move and keep the best one
                if (position.CanPlay(columnOrder[x]))
                {
                    IPosition newPosition = position.Clone();
                    newPosition.Play(columnOrder[x]);               // It's opponent turn in P2 position after current player plays x column.
                    int score = -Negamax(newPosition, -beta, -alpha); // explore opponent's score within [-beta;-alpha] windows:
                                                                      // no need to have good precision for score better than beta (opponent's score worse than -beta)
                                                                      // no need to check for score worse than alpha (opponent's score worse better than -alpha)
                    if (score >= beta)
                        return score;  // prune the exploration if we find a possible move better than what we were looking for.
                    if (score > alpha)
                        alpha = score; // reduce the [alpha;beta] window for next exploration, as we only 
                                       // need to search for a position that is better than the best so far.
                }

            return alpha;
        }
    }
}
