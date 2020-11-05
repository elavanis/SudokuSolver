using Game.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    public class Board
    {
        private Rules AI { get; } = new Rules();

        public Cell[,] BoardLayout { get; private set; } = new Cell[9, 9];

        public Board()
        {
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    BoardLayout[x, y] = new Cell(x,y);
                    
                }
            }
        }

        public void SetValue(int x, int y, int value)
        {
            lock (BoardLayout)
            {
                Cell cell = BoardLayout[x, y];
                cell.SetValue(value);
            }
        }

        public void FigureStuffOut()
        {
            lock (BoardLayout)
            {
                bool loop = true;
                while (loop)
                {
                    loop = false;
                    for (int x = 0; x < 9; x++)
                    {
                        for (int y = 0; y < 9; y++)
                        {
                            Cell cell = BoardLayout[x, y];
                            if (
                                RunAI(x, y, cell, NeighborsX(x, y)) ||
                                RunAI(x, y, cell, NeighborsY(x, y)) ||
                                RunAI(x, y, cell, NeighborsCube(x, y)))
                            {
                                loop = true;
                            }
                        }
                    }
                }
            }
        }


        private List<ICell> NeighborsX(int x, int y)
        {
            List<ICell> neighbors = new List<ICell>();
            for (int i = 0; i < 9; i++)
            {
                neighbors.Add(BoardLayout[x, i]);
            }

            neighbors.Remove(BoardLayout[x, y]);

            return neighbors;
        }
        private List<ICell> NeighborsY(int x, int y)
        {
            List<ICell> neighbors = new List<ICell>();
            for (int i = 0; i < 9; i++)
            {
                neighbors.Add(BoardLayout[i, y]);
            }

            neighbors.Remove(BoardLayout[x, y]);

            return neighbors;
        }

        private List<ICell> NeighborsCube(int x, int y)
        {
            List<ICell> neighbors = new List<ICell>();
            int xStart = x / 3;
            int yStart = y / 3;

            for (int xPos = 0; xPos < 3; xPos++)
            {
                for (int yPos = 0; yPos < 3; yPos++)
                {
                    neighbors.Add(BoardLayout[xStart * 3 + xPos, yStart * 3 + yPos]);
                }
            }

            neighbors.Remove(BoardLayout[x, y]);

            return neighbors;
        }


        private bool RunAI(int x, int y, ICell cell, List<ICell> neighbors)
        {
            RulesResult aIResult = AI.Process(cell, neighbors);
            if (aIResult.Value != null)
            {
                cell.SetValue((int)aIResult.Value);
            }
            else
            {
                foreach (var item in aIResult.RemovedPossible)
                {
                    cell.RemovePossibleValue(item);
                }
            }

            if (aIResult.Value != null
                || aIResult.RemovedPossible.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
