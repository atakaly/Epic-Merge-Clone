using EpicMergeClone.Game.Items;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace EpicMergeClone.Game.Mechanics.Grid
{
    public class CellGrid : MonoBehaviour
    {
        private List<Cell> m_Grid;

        [SerializeField] private int width;
        [SerializeField] private int heigth;

        [SerializeField] private Cell cellPrefab;

        private ItemPool<ItemBase> m_BaseItemPool;
        private ItemPool<CollectibleItem> m_CollectibleItemPool;
        private ItemPool<ProductionItem> m_ProductionItemPool;

        private AllItemDatas m_AllItemDatas;

        [Inject]
        public void Construct(ItemPool<ItemBase> baseItemPool, 
            ItemPool<CollectibleItem> collectibleItemPool, 
            ItemPool<ProductionItem> productionItemPool,
            AllItemDatas allItemDatas)
        {
            m_BaseItemPool = baseItemPool;
            m_CollectibleItemPool = collectibleItemPool;
            m_ProductionItemPool = productionItemPool;
            m_AllItemDatas = allItemDatas;
        }

        private void SetGrid()
        {
            GameState.GridData gridData = GameState.LoadGridData();

            for (int i = 0; i < gridData.cells.Length; i++)
            {
                var currentCellData = gridData.cells[i];
                m_Grid[i].AddItem(m_BaseItemPool.SpawnItem(m_AllItemDatas.GetItemData(currentCellData.itemData.itemId)));
            }

            for (int i = 0; i < m_Grid.Count; i++)
            {
            }
        }


        #region Editor Utilities

        public void CreateGrid()
        {
            float cellWidth = 1.2f;
            float cellHeight = .27f;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < heigth; j++)
                {
                    float xPos = i * cellWidth;
                    float yPos = j * cellHeight;

                    xPos += (j % 2 == 1) ? cellWidth * 0.5f : 0;

                    var newCell = Instantiate(cellPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity, transform);
                    newCell.X = i;
                    newCell.Y = j;
                    newCell.State = CellState.Available;

                    m_Grid.Add(newCell);
                }
            }

            SetNeighbours();
        }

        private void SetNeighbours()
        {
            foreach (Cell cell in m_Grid)
            {
                cell.NeighbourCells.Clear();

                int currentX = cell.X;
                int currentY = cell.Y;

                foreach (Cell otherCell in m_Grid)
                {
                    int otherX = otherCell.X;
                    int otherY = otherCell.Y;

                    if (otherCell == cell)
                        continue;

                    if (IsNeighbor(currentX, currentY, otherX, otherY))
                    {
                        cell.NeighbourCells.Add(otherCell);
                    }
                }
            }
        }

        private bool IsNeighbor(int x1, int y1, int x2, int y2)
        {
            int deltaX = Mathf.Abs(x1 - x2);
            int deltaY = Mathf.Abs(y1 - y2);

            return (deltaX == 1 && deltaY == 0) || (deltaX == 0 && deltaY == 1) || (deltaX == 1 && deltaY == 1);
        }

        #endregion
    }
}