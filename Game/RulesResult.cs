using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    public class RulesResult
    {
        public int? Value { get; set; } = null;
        public List<int> RemovedPossible { get; set; } = new List<int>();
    }
}
