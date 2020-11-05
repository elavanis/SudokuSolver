using Game.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    public class Rules
    {
        //private Dictionary<string, List<int>> ListSets { get; } = new Dictionary<string, List<int>>();
        private List<List<int>> ListSets { get; }

        public Rules()
        {
            ListSets = GenerateListSets();
        }

        private List<List<int>> GenerateListSets()
        {
            Dictionary<string, List<int>> dictionarySets = new Dictionary<string, List<int>>();

            for (int i = 1; i < 10; i++)
            {
                dictionarySets.Add(i.ToString(), new List<int>() { i });
            }

            for (int i = 2; i < 9; i++)
            {
                List<string> keys = GetKeys(i, dictionarySets);

                foreach (var key in keys)
                {
                    List<int> list = dictionarySets[key];
                    int max = 0;
                    foreach (var item in list)
                    {
                        max = Math.Max(max, item);
                    }

                    for (int j = max + 1; j < 10; j++)
                    {
                        List<int> newList = new List<int>(list);
                        newList.Add(j);
                        dictionarySets.Add(key + j, newList);
                    }
                }
            }


            List<List<int>> listSets = new List<List<int>>();
            foreach (var item in dictionarySets.Values)
            {
                if (item.Count > 1)
                {
                    listSets.Add(item);
                }
            }

            return listSets;
        }

        private List<string> GetKeys(int i, Dictionary<string, List<int>> dictionarySets)
        {
            List<string> keys = new List<string>();
            foreach (var item in dictionarySets.Keys)
            {
                if (item.Length == i - 1)
                {
                    keys.Add(item);
                }
            }

            return keys;
        }

        public RulesResult Process(ICell cell, List<ICell> neighbors)
        {
            RulesResult result = new RulesResult();

            if (cell.PossibleValues.Count > 0)
            {
                result = SetWhenOnly1ValueLeft(cell, neighbors, result);

                result = RemoveOtherSetValues(cell, neighbors, result);

                result = FindOnlyValue(cell, neighbors, result);

                result = FindSetsInNeigbors(cell, neighbors, result);
            }

            return result;
        }

        private RulesResult SetWhenOnly1ValueLeft(ICell cell, List<ICell> neighbors, RulesResult result)
        {
            if (cell.PossibleValues.Count == 1)
            {
                result.Value = cell.PossibleValues[0];
            }

            return result;
        }

        private RulesResult RemoveOtherSetValues(ICell cell, List<ICell> neighbors, RulesResult result)
        {
            foreach (ICell item in neighbors)
            {
                if (item.Value != null)
                {
                    cell.RemovePossibleValue((int)item.Value);
                }
            }

            return result;
        }

        private RulesResult FindOnlyValue(ICell cell, List<ICell> neighbors, RulesResult result)
        {
            foreach (int value in cell.PossibleValues)
            {
                bool onlyValue = true;
                foreach (ICell localCell in neighbors)
                {
                    if (localCell.PossibleValues.Contains(value))
                    {
                        onlyValue = false;
                        break;
                    }
                }

                if (onlyValue)
                {
                    result.Value = value;
                    break;
                }
            }

            return result;
        }

        private RulesResult FindSetsInNeigbors(ICell cell, List<ICell> neighbors, RulesResult result)
        {
            List<ICell> smallList = new List<ICell>();
            smallList.AddRange(neighbors);
            smallList.Add(cell);
            FindSetsInBigNeighbors(cell, smallList, result);

            return result;
        }

        private void FindSetsInBigNeighbors(ICell cell, List<ICell> neighbors, RulesResult result)
        {
            foreach (var listSet in ListSets)
            {
                List<ICell> possibleNeigbhors = new List<ICell>(neighbors);
                List<int> removeValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                foreach (var value in listSet)
                {
                    removeValues.Remove(value);
                }

                //remove anything that has values we don't want or is already set
                foreach (var valueToRemove in removeValues)
                {
                    for (int i = possibleNeigbhors.Count-1; i >=0; i--)
                    {
                        if (possibleNeigbhors[i].PossibleValues.Contains(valueToRemove) || possibleNeigbhors[i].PossibleValues.Count == 0)
                        {
                            possibleNeigbhors.RemoveAt(i);
                        }
                    }
                }

                //we have enough cells that contain only the values we need to remove the values from the neighbors
                if (possibleNeigbhors.Count >= listSet.Count)
                {
                    //verify the current cell is not part of the matched set so we only remove it from a neighbor
                    if (!possibleNeigbhors.Contains(cell))
                    {
                        for (int i = 0; i < listSet.Count; i++)
                        {
                            if (cell.PossibleValues.Contains(listSet[i]))
                            {
                                result.RemovedPossible.Add(listSet[i]);
                            }
                        }
                    }
                }
            }
        }

        private void RemovePossibleValuesFromOtherCells(ICell cell, List<ICell> neighbors)
        {
            foreach (Cell neighbor in neighbors)
            {
                bool totalMatch = true;
                if (neighbor.PossibleValues != cell.PossibleValues)
                {
                    totalMatch = false;
                }
                else
                {
                    foreach (int value in cell.PossibleValues)
                    {
                        if (!neighbor.PossibleValues.Contains(value))
                        {
                            totalMatch = false;
                            break;
                        }
                    }
                }

                if (!totalMatch)
                {
                    foreach (int value in cell.PossibleValues)
                    {
                        neighbor.PossibleValues.Remove(value);
                    }
                }
            }
        }
    }
}
