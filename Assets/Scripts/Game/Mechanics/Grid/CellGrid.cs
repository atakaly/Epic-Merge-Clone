using EpicMergeClone.Installers;
using EpicMergeClone.Pool;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace EpicMergeClone.Game.Mechanics.Grid
{
    public class CellGrid : MonoBehaviour
    {
        public List<Cell> m_Cells;

        public int width;
        public int heigth;

        public Cell cellPrefab;

        private ItemPoolManager m_ItemPoolManager;

        private GlobalGameData m_GlobalGameData;

        [Inject]
        public void Construct(ItemPoolManager itemPoolManager,
            GlobalGameData globalGameData)
        {
            m_ItemPoolManager = itemPoolManager;
            m_GlobalGameData = globalGameData;
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
                var itemData = m_GlobalGameData.allItemDatas.GetItemData(currentCellData.itemData.itemId);

                m_Cells[i].OnCellStateChanged += SaveState;

                if (itemData == null)
                    continue;

                m_Cells[i].AddItem(m_ItemPoolManager.SpawnItem(itemData), m_Cells[i].transform.position, m_Cells[i].transform.position);
                m_Cells[i].CurrentItem.LateInitialize();
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

            for (int i = 0; i < m_Cells.Count; i++)
            {
                GameState.ItemData thisCellItemData = new GameState.ItemData()
                {
                    itemId = m_Cells[i].CurrentItem == null ? "" : m_Cells[i].CurrentItem.ItemDataSO.ItemId
                };

                GameState.CellData newCellData = new GameState.CellData()
                {
                    itemData = thisCellItemData,
                    x = m_Cells[i].X,
                    y = m_Cells[i].Y,
                    state = m_Cells[i].State
                };

                currentCellDatas.Add(newCellData);
            }

            gridData.cells = currentCellDatas.ToArray();

            return gridData;
        }

        #region Editor Utilities


        public void SetNeighbours()
        {
            foreach (Cell cell in m_Cells)
            {
                cell.NeighbourCells.Clear();

                int currentX = cell.X;
                int currentY = cell.Y;

                foreach (Cell otherCell in m_Cells)
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