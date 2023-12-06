using EpicMergeClone.Game.Items;
using EpicMergeClone.Game.Mechanics.Grid;
using EpicMergeClone.Pool;
using System.Collections.Generic;

namespace EpicMergeClone.Game.Mechanics.Board
{
    public static class MergeManager
    {
        public static void Merge(List<ItemBase> mergingItems,
            ItemPoolManager poolManager,
            AllItemDatas allItemsData)
        {
            var targetItem = mergingItems[0];
            var targetCell = targetItem.CurrentCell;
            var targetItemData = targetItem.ItemDataSO;

            for (int i = 0; i < mergingItems.Count; i++)
            {
                var currentItem = mergingItems[i];
                currentItem.CurrentCell.RemoveItem();
                poolManager.DespawnItem(currentItem);
            }

            var itemTreeSO = allItemsData.GetItemTreeOf(targetItem.ItemDataSO);
            
            ItemBase newItem = poolManager.SpawnItem(targetItem);
            targetCell.AddItem(newItem);
            newItem.InitializeItem(itemTreeSO.GetNextItemData(targetItemData));
        }

        public static List<ItemBase> TryGetMergeItems(ItemBase currentItem, ItemBase targetItem)
        {
            if (targetItem == null)
                return null;

            List<Cell> visitedCells = new List<Cell>();
            List<Cell> mergingCells = FindMergingCells(currentItem, targetItem, visitedCells);

            if (mergingCells.Count < 2)
                return null;
            
            List<ItemBase> mergingItems = new List<ItemBase>();

            mergingItems.Add(targetItem);
            mergingItems.Add(currentItem);

            for (int i = 0; i < mergingCells.Count; i++)
            {
                var cell = mergingCells[i];
                cell.CurrentItem.Move(targetItem.transform.position, 0.2f);
                
                if(!mergingItems.Contains(cell.CurrentItem))
                    mergingItems.Add(cell.CurrentItem);
            }

            return mergingItems;
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