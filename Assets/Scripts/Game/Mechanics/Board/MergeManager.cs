using EpicMergeClone.Game.Items;
using EpicMergeClone.Game.Mechanics.Grid;
using EpicMergeClone.Pool;
using System.Collections.Generic;
using UnityEngine;
using static EpicMergeClone.GameState;

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
                currentItem.Move(targetCell.transform.position, 0.2f, () =>
                {
                    poolManager.DespawnItem(currentItem);
                });
            }

            var targetItemTreeSO = allItemsData.GetItemTreeOf(targetItem.ItemDataSO);
            var nextItemData = targetItemTreeSO.GetNextItemData(targetItemData);

            int totalItemCount = mergingItems.Count;
            int nextItemCount = totalItemCount / 3;
            int sameItemCount = totalItemCount % 3;

            for (int i = 0; i < nextItemCount; i++)
            {
                var nextItem = poolManager.SpawnItem(nextItemData);
                nextItem.InitializeItem(nextItemData);
                var cellToSpawn = targetCell.GetFirstAvailableNeighbour();
                cellToSpawn.AddItem(nextItem, targetCell.transform.position, cellToSpawn.transform.position);
            }

            for (int i = 0; i < sameItemCount; i++)
            {
                var normalItem = poolManager.SpawnItem(targetItemData);
                normalItem.InitializeItem(targetItemData);
                var cellToSpawn = targetCell.GetFirstAvailableNeighbour();
                cellToSpawn.AddItem(normalItem, targetCell.transform.position, cellToSpawn.transform.position);
            }
        }

        public static List<ItemBase> GetMergeItems(ItemBase currentItem, ItemBase targetItem)
        {
            if (targetItem == null)
                return null;

            List<Cell> visitedCells = new List<Cell>();
            List<Cell> mergingCells = FindMergingCells(currentItem, targetItem, visitedCells);

            if (mergingCells.Count < 1)
                return null;

            List<ItemBase> mergingItems = new List<ItemBase>
            {
                targetItem,
                currentItem
            };

            for (int i = 0; i < mergingCells.Count; i++)
            {
                var cell = mergingCells[i];
                
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
                    neighbourCell.CurrentItem.ItemDataSO.IsSameType(currentItem.ItemDataSO) &&
                    targetItem.ItemDataSO.IsSameType(currentItem.ItemDataSO))
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