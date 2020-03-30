using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public interface IPosition
    {
        int Width { get; }

        int Height { get; }

        int NbMoves { get; }

        bool CanPlay(int col);

        void Play(int col);

        int Play(char[] moves);


        bool IsWinningMove(int col);

        IPosition Clone();
    }
}
