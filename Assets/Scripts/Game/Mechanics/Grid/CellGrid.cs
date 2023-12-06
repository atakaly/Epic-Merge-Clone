using EpicMergeClone.Game.Items;
using EpicMergeClone.Pool;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace EpicMergeClone.Game.Mechanics.Grid
{
    public class CellGrid : MonoBehaviour
    {
        private event Action OnGridStateChanged;

        [SerializeField] private List<Cell> m_Grid;

        [SerializeField] private int width;
        [SerializeField] private int heigth;

        [SerializeField] private Cell cellPrefab;

        private ItemPoolManager m_ItemPoolManager;

        private AllItemDatas m_AllItemDatas;

        [Inject]
        public void Construct(ItemPoolManager itemPoolManager,
            AllItemDatas allItemDatas)
        {
            m_ItemPoolManager = itemPoolManager;
            m_AllItemDatas = allItemDatas;
        }

        public void Start()
        {
            SetGrid();
        }

        private void SetGrid()
        {
            GameState.GridData gridData = GameState.LoadGridData();

            for (int i = 0; i < gridData.cells.Length; i++)
            {
                var currentCellData = gridData.cells[i];
                var itemData = m_AllItemDatas.GetItemData(currentCellData.itemData.itemId);

                m_Grid[i].OnItemAdded += SaveState;

                if (itemData == null)
                    continue;

                m_Grid[i].AddItem(m_ItemPoolManager.SpawnItem(itemData));
            }
        }

        private void SaveState()
        {
            GameState.SaveGridState(GetGridData());
        }

        private GameState.GridData GetGridData()
        {
            GameState.GridData gridData = new GameState.GridData();

            List<GameState.CellData> currentCellDatas = new List<GameState.CellData>();

            for (int i = 0; i < m_Grid.Count; i++)
            {
                GameState.ItemBaseData thisCellItemData = new GameState.ItemBaseData()
                {
                    itemId = m_Grid[i].CurrentItem == null ? "" : m_Grid[i].CurrentItem.ItemDataSO.UniqueId
                };

                GameState.CellData newCellData = new GameState.CellData()
                {
                    itemData = thisCellItemData,
                    x = m_Grid[i].X,
                    y = m_Grid[i].Y,
                    state = m_Grid[i].State
                };

                currentCellDatas.Add(newCellData);
            }

            gridData.cells = currentCellDatas.ToArray();

            return gridData;
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

        public void SaveCurrentGridState()
        {
            GameState.SaveGridState(GetGridData());
        }

        #endregion
    }
}