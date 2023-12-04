using EpicMergeClone.Game.Items;
using EpicMergeClone.Game.Mechanics.Grid;
using System.Collections.Generic;

namespace EpicMergeClone.Game.Mechanics.Board
{
    public static class MergeManager
    {
        public static bool TryMerge(ItemBase currentItem, ItemBase targetItem)
        {
            if (targetItem == null)
                return false;

            List<Cell> visitedCells = new List<Cell>();
            List<Cell> mergingCells = FindMergingCells(currentItem, targetItem, visitedCells);

            if (mergingCells.Count < 2)
                return false;

            for (int i = 0; i < mergingCells.Count; i++)
            {
                var cell = mergingCells[i];
                cell.CurrentItem.Move(targetItem.transform.position, 0.2f);
                //Create new item, get from pool
            }

            return true;
        }

        private static List<Cell> FindMergingCells(ItemBase currentItem, ItemBase targetItem, List<Cell> visitedCells)
        {
            List<Cell> mergingCells = new List<Cell>();

            Cell startCell = targetItem.CurrentCell;

            visitedCells.Add(startCell);

            for (int i = 0; i < startCell.NeighbourCells.Count; i++)
            {
                Cell neighbourCell = startCell.NeighbourCells[i];

                if (visitedCells.Contains(neighbourCell))
                    continue;

                if (neighbourCell.CurrentItem != null &&
                    neighbourCell.CurrentItem.ItemDataSO.IsSameType(currentItem.ItemDataSO))
                {
                    mergingCells.Add(neighbourCell);
                    mergingCells.AddRange(FindMergingCells(currentItem, neighbourCell.CurrentItem, visitedCells));
                }

                visitedCells.Add(neighbourCell);
            }

            return mergingCells;
        }
    }
}