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

        public void AddItem(ItemBase item, Vector3 startPosition, Vector3 endPosition)
        {
            m_CurrentItem = item;
            m_CurrentItem.transform.position = startPosition;
            m_CurrentItem.Move(endPosition, 0.3f);
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
            Cell availableCell = GetFirstAvailableNeighbour();
            ItemBase oldItem = m_CurrentItem;

            availableCell.AddItem(oldItem, oldItem.transform.position, availableCell.transform.position);
            oldItem.CurrentCell = availableCell;
        }

        public Cell GetFirstAvailableNeighbour()
        {
            Queue<Cell> queue = new Queue<Cell>();
            HashSet<Cell> visitedCells = new HashSet<Cell>();

            queue.Enqueue(this);
            visitedCells.Add(this);

            while (queue.Count > 0)
            {
                Cell currentCell = queue.Dequeue();

                foreach (Cell neighbor in currentCell.NeighbourCells)
                {
                    if (!visitedCells.Contains(neighbor))
                    {
                        visitedCells.Add(neighbor);

                        if (neighbor.State == CellState.Available)
                            return neighbor;

                        queue.Enqueue(neighbor);
                    }
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