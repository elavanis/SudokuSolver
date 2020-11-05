using Game.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    public class Cell : ICell
    {
        public int X { get; }
        public int Y { get; }

        public int? Value { get; private set; } = null;
        public List<int> PossibleValues { get; } = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void SetValue(int value)
        {
            Value = value;
            PossibleValues.Clear();
        }

        public void RemovePossibleValue(int value)
        {
            PossibleValues.Remove(value);
        }
    }
}
