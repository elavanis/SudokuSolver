using System.Collections.Generic;

namespace Game.Interface
{
    public interface ICell
    {
        int X { get; }
        int Y { get; }

        List<int> PossibleValues { get; }
        int? Value { get; }

        void RemovePossibleValue(int value);
        void SetValue(int value);
    }
}