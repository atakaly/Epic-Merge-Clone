using DG.Tweening;
using EpicMergeClone.Game.Mechanics.Board;
using EpicMergeClone.Game.Mechanics.Grid;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EpicMergeClone.Game.Items
{
    public class ItemBase : MonoBehaviour
    {
        [SerializeField] public ItemDataSO ItemDataSO;

        [SerializeField] private SpriteRenderer m_SpriteRenderer;

        private List<Cell> m_CollidingCells = new List<Cell>();

        public Cell CurrentCell { get; set; }

        private void Awake()
        {
            CurrentCell = GetComponentInParent<Cell>();
        }

        public void InitializeItem(ItemDataSO itemData)
        {
            ItemDataSO = itemData;

            m_SpriteRenderer.sprite = itemData.itemSprite;
        }

        public void Move(Vector3 position, float duration, Action onComplete = null)
        {
            transform.DOKill(true);
            transform.DOMove(position, duration)
                .OnComplete(() => onComplete?.Invoke());
        }

        private void OnMouseDrag()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            transform.position = mousePosition;
        }

        private void OnMouseUp()
        {
            if (m_CollidingCells.Count == 0)
            {
                CurrentCell.AddItem(this);
                return;
            }

            Cell targetCell = m_CollidingCells[m_CollidingCells.Count - 1];

            if (targetCell.State == CellState.Unavailable || targetCell.State == CellState.Locked)
            {
                CurrentCell.AddItem(this);
                return;
            }

            if(MergeManager.TryMerge(this, targetCell.CurrentItem))
            {
                Debug.Log("Merged");
            } 
            else
            {
                if (targetCell.State == CellState.Occupied)
                {
                    targetCell.ShiftItem();
                }

                CurrentCell.RemoveItem();
                targetCell.AddItem(this);
                CurrentCell = targetCell;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Cell cell))
            {
                m_CollidingCells.Add(cell);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Cell cell))
            {
                m_CollidingCells.Remove(cell);
            }
        }
    }
}