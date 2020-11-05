using Game;
using System;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();

            board.SetValue(0, 0, 8);
            board.SetValue(1, 0, 7);
            board.SetValue(2, 0, 6);

            board.SetValue(1, 1, 1);
            board.SetValue(5, 1, 6);

            board.SetValue(1, 2, 4);
            board.SetValue(3, 2, 3);
            board.SetValue(5, 2, 5);
            board.SetValue(6, 2, 8);


            board.SetValue(0, 3, 4);
            board.SetValue(6, 3, 2);
            board.SetValue(7, 3, 1);

            board.SetValue(1, 4, 9);
            board.SetValue(3, 4, 5);

            board.SetValue(1, 5, 5);
            board.SetValue(4, 5, 4);
            board.SetValue(6, 5, 3);
            board.SetValue(8, 5, 6);


            board.SetValue(1, 6, 2);
            board.SetValue(2, 6, 9);
            board.SetValue(8, 6, 8);

            board.SetValue(2, 7, 4);
            board.SetValue(3, 7, 6);
            board.SetValue(4, 7, 9);
            board.SetValue(6, 7, 1);
            board.SetValue(7, 7, 7);
            board.SetValue(8, 7, 3);

            board.SetValue(5, 8, 1);
            board.SetValue(8, 8, 4);

            while (true)
            {
                board.FigureStuffOut();
            }
        }
    }
}
