using EpicMergeClone.Game.Items;
using UnityEngine;

namespace EpicMergeClone.Game.Mechanics.Grid
{
    public class Cell : MonoBehaviour
    {
        private int m_X;
        private int m_Y;

        private CellState m_CellState;
        private ItemBase m_CurrentItem;

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

        private void Awake()
        {
            m_CellState = CellState.Available;
        }

        public void AddItem(ItemBase item)
        {
            item.transform.SetParent(transform);
            item.transform.localPosition = Vector3.zero;
            m_CurrentItem = item;
        }

        public void RemoveItem()
        {
            m_CurrentItem = null;
        }
    }
}