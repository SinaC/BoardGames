using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public class MatrixBoard : IPosition
    {
        private const int width = 7;
        private const int height = 6;

        private int[,] board = new int[width,height]; // 0 if cell is empty, 1 for first player and 2 for second player.
        private int[] heights = new int[width];        // number of stones per column
        private int moves;       // number of moves played since the beinning of the

        public int Width => width;

        public int Height => height;

        /**    
         * @return number of moves played from the beginning of the game.
         */
        public int NbMoves => moves;

        /**
        * Indicates whether a column is playable.
        * @param col: 0-based index of column to play
        * @return true if the column is playable, false if the column is already full.
        */
        public bool CanPlay(int col) => heights[col] < Height;

        /**
         * Plays a playable column.
         * This function should not be called on a non-playable column or a column making an alignment.
         *
         * @param col: 0-based index of a playable column.
         */
        public void Play(int col)
        {
            board[col, heights[col]] = 1 + moves % 2;
            heights[col]++;
            moves++;
        }

        /*
         * Plays a sequence of successive played columns, mainly used to initilize a board.
         * @param seq: a sequence of digits corresponding to the 1-based index of the column played.
         *
         * @return number of played moves. Processing will stop at first invalid move that can be:
         *           - invalid character (non digit, or digit >= WIDTH)
         *           - playing a colum the is already full
         *           - playing a column that makes an aligment (we only solve non).
         *         Caller can check if the move sequence was valid by comparing the number of 
         *         processed moves to the length of the sequence.
         */
        public int Play(char[] moves)
        {
            for (int i = 0; i < moves.Length; i++)
            {
                int col = moves[i] - '1';
                if (col < 0 || col >= Width || !CanPlay(col) || IsWinningMove(col))
                    return i; // invalid move
                Play(col);
            }
            return moves.Length;
        }

        /**
         * Indicates whether the current player wins by playing a given column.
         * This function should never be called on a non-playable column.
         * @param col: 0-based index of a playable column.
         * @return true if current player makes an alignment by playing the corresponding column col.
         */
        public bool IsWinningMove(int col)
        {
            int current_player = 1 + moves % 2;
            // check for vertical alignments
            if (heights[col] >= 3
                && board[col, heights[col] - 1] == current_player
                && board[col, heights[col] - 2] == current_player
                && board[col, heights[col] - 3] == current_player)
                return true;

            for (int dy = -1; dy <= 1; dy++) {    // Iterate on horizontal (dy = 0) or two diagonal directions (dy = -1 or dy = 1).
                int nb = 0;                       // counter of the number of stones of current player surronding the played stone in tested direction.
                for (int dx = -1; dx <= 1; dx += 2) // count continuous stones of current player on the left, then right of the played column.
                    for (int x = col + dx, y = heights[col] + dx * dy; x >= 0 && x < Width && y >= 0 && y < Height && board[x, y] == current_player; nb++) {
                        x += dx;
                        y += dx * dy;
                    }
                if (nb >= 3) return true; // there is an aligment if at least 3 other stones of the current user 
                                          // are surronding the played stone in the tested direction.
            }
            return false;
        }

        //
        public IPosition Clone()
        {
            return new MatrixBoard
            {
                board = board.Clone() as int[,],
                heights = heights.Clone() as int[],
                moves = moves
            };
        }

        //
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int h = Height - 1; h >= 0; h--)
            {
                for (int w = 0; w < Width; w++)
                    sb.Append(board[w, h]);
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
