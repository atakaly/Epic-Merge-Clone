using EpicMergeClone.Game.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EpicMergeClone.Game.Mechanics.Grid
{
    public class Cell : MonoBehaviour
    {
        public event Action OnCellStateChanged;

        [SerializeField] private int m_X;
        [SerializeField] private int m_Y;
        [SerializeField] private CellState m_CellState;

        [SerializeField] private ItemBase m_CurrentItem;
        [SerializeField] private List<Cell> m_NeighbourCells;

        public int X
        {
            get { return m_X; }
            set { m_X = value; }
        }

        public int Y
        {
            get { return m_Y; }
            set { m_Y = value; }
        }

        public CellState State
        {
            get { return m_CellState; }
            set { m_CellState = value; }
        }

        public ItemBase CurrentItem
        {
            get { return m_CurrentItem; }
            private set { m_CurrentItem = value; }
        }

        public List<Cell> NeighbourCells
        {
            get { return m_NeighbourCells; }
            set { m_NeighbourCells = value; }
        }

        private void Awake()
        {
            m_CellState = m_CurrentItem != null ? CellState.Occupied : CellState.Available;
        }

        public void AddItem(ItemBase item)
        {
            m_CurrentItem = item;
            m_CurrentItem.Move(transform.position, 0.3f);
            m_CurrentItem.CurrentCell = this;
            m_CellState = CellState.Occupied;

            OnCellStateChanged?.Invoke();
        }

        public void RemoveItem()
        {
            m_CurrentItem = null;
            m_CellState = CellState.Available;

            OnCellStateChanged?.Invoke();
        }

        public void ShiftItem()
        {
            List<Cell> visitedCells = new List<Cell>();
            Cell availableCell = GetFirstAvailableNeighbour(visitedCells);
            ItemBase oldItem = m_CurrentItem;

            m_CurrentItem.Move(availableCell.transform.position, 0.3f, () => availableCell.AddItem(oldItem));
        }

        public Cell GetFirstAvailableNeighbour(List<Cell> visitedCells)
        {
            for (int i = 0; i < NeighbourCells.Count; i++)
            {
                Cell neighbor = NeighbourCells[i];
                if (neighbor.State == CellState.Available && !visitedCells.Contains(neighbor))
                    return neighbor;
            }

            for (int i = 0; i < NeighbourCells.Count; i++)
            {
                Cell neighbor = NeighbourCells[i];
                if (neighbor.State == CellState.Occupied && !visitedCells.Contains(neighbor))
                {
                    visitedCells.Add(neighbor);
                    Cell availableNeighbor = neighbor.GetFirstAvailableNeighbour(visitedCells);
                    if (availableNeighbor != null)
                        return availableNeighbor;
                }
            }

            return null;
        }

        public override string ToString()
        {
            return ("Cell_" + m_X.ToString() + "_" + m_Y.ToString());
        }
    }
}