namespace Connect4
{
    //http://blog.gamesolver.org/solving-connect-four/06-bitboard/
    /** 
     * A class storing a Connect 4 position.
     * Functions are relative to the current player to play.
     * Position containing aligment are not supported by this class.
     *
     * A binary bitboard representationis used.
     * Each column is encoded on HEIGHT+1 bits.
	   * 
     * Example of bit order to encode for a 7x6 board
	   * .  .  .  .  .  .  .
	   * 5 12 19 26 33 40 47
	   * 4 11 18 25 32 39 46
	   * 3 10 17 24 31 38 45
	   * 2  9 16 23 30 37 44
	   * 1  8 15 22 29 36 43
	   * 0  7 14 21 28 35 42 
	   * 
     * Position is stored as
     * - a bitboard "mask" with 1 on any color stones
     * - a bitboard "current_player" with 1 on stones of current player
     *
     * "current_player" bitboard can be transformed into a compact and non ambiguous key
     * by adding an extra bit on top of the last non empty cell of each column.
     * This allow to identify all the empty cells whithout needing "mask" bitboard
     *
	   * current_player "x" = 1, opponent "o" = 0
	   * board     position  mask      key       bottom
	   *           0000000   0000000   0000000   0000000
	   * .......   0000000   0000000   0001000   0000000
	   * ...o...   0000000   0001000   0010000   0000000
	   * ..xx...   0011000   0011000   0011000   0000000
	   * ..ox...   0001000   0011000   0001100   0000000
	   * ..oox..   0000100   0011100   0000110   0000000
	   * ..oxxo.   0001100   0011110   1101101   1111111
	   *
	   * current_player "o" = 1, opponent "x" = 0
	   * board     position  mask      key       bottom
	   *           0000000   0000000   0001000   0000000
	   * ...x...   0000000   0001000   0000000   0000000
	   * ...o...   0001000   0001000   0011000   0000000
	   * ..xx...   0000000   0011000   0000000   0000000
	   * ..ox...   0010000   0011000   0010100   0000000
	   * ..oox..   0011000   0011100   0011010   0000000
	   * ..oxxo.   0010010   0011110   1110011   1111111
	   *
	   * key is an unique representation of a board key = position + mask + bottom
	   * in practice, as bottom is constant, key = position + mask is also a 
     * non-ambigous representation of the position.
	 */
    public class BitBoard : IPosition
    {
        private const ulong ulong1 = (ulong)1;
        private const int width = 7;
        private const int height = 6;

        private ulong currentPosition;
        private ulong mask;
        private int moves; // number of moves played since the beginning of the game

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
        public bool CanPlay(int col) => (mask & TopMask(col)) == 0;

        /**
        * Plays a playable column.
        * This function should not be called on a non-playable column or a column making an alignment.
        *
        * @param col: 0-based index of a playable column.
        */
        public void Play(int col)
        {
            currentPosition ^= mask;
            mask |= mask + BottomMask(col);
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
                if (col < 0 || col >= width || !CanPlay(col) || IsWinningMove(col))
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
            ulong pos = currentPosition | ((mask + BottomMask(col)) & ColumnMask(col));
            return Alignment(pos);
        }

        //
        public IPosition Clone() 
        {
            return new BitBoard
            {
                currentPosition = currentPosition,
                mask = mask,
                moves = moves
            };
        }

        /**    
        * @return a compact representation of a position on WIDTH*(HEIGHT+1) bits.
        */
        public ulong Key => currentPosition + mask;

        /**
        * Test an alignment for current player (identified by one in the bitboard pos)
        * @param a bitboard position of a player's cells.
        * @return true if the player has a 4-alignment.
        */
        private bool Alignment(ulong pos)
        {
            ulong m;

            // horizontal 
            m = pos & (pos >> (height + 1));
            if ((m & (m >> (2 * (height + 1)))) != 0)
                return true;

            // diagonal 1
            m = pos & (pos >> height);
            if ((m & (m >> (2 * height))) != 0)
                return true;

            // diagonal 2 
            m = pos & (pos >> (height + 2));
            if ((m & (m >> (2 * (height + 2)))) != 0)
                return true;

            // vertical;
            m = pos & (pos >> 1);
            if ((m & (m >> 2)) != 0)
                return true;

            return false;
        }

        private ulong TopMask(int col) => ((ulong1 << (height - 1)) << (col * (height + 1)));
        
        private ulong BottomMask(int col) => ulong1 << (col * (height + 1));
        
        private ulong ColumnMask(int col) => ((ulong1 << height) - ulong1) << (col * (height + 1));
    }
}
