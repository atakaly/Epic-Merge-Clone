using DG.Tweening;
using EpicMergeClone.Game.Mechanics;
using EpicMergeClone.Game.Mechanics.Board;
using EpicMergeClone.Game.Mechanics.Grid;
using EpicMergeClone.Installers;
using EpicMergeClone.Pool;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace EpicMergeClone.Game.Items
{
    public class ItemBase : MonoBehaviour
    {
        [SerializeField] public ItemDataSO ItemDataSO;
        [SerializeField] private SpriteRenderer m_SpriteRenderer;

        private ItemInputHandler m_ItemInputHandler;

        private List<Cell> m_CollidingCells = new List<Cell>();

        public Cell CurrentCell { get; set; }

        protected ItemPoolManager m_ItemPoolManager;
        protected GlobalGameData m_GlobalGameData;
        protected GameStateManager m_GameStateManager;

        [Inject]
        public void Construct(ItemPoolManager itemPoolManager,
            GlobalGameData globalGameData,
            GameStateManager gameStateManager)
        {
            m_ItemPoolManager = itemPoolManager;
            m_GlobalGameData = globalGameData;
            m_GameStateManager = gameStateManager;
        }

        private void Start()
        {
            m_ItemInputHandler = GetComponent<ItemInputHandler>();

            m_ItemInputHandler.OnClick += OnClick;
            m_ItemInputHandler.OnDragged += OnDragged;
        }

        public virtual void InitializeItem(ItemDataSO itemData)
        {
            ItemDataSO = itemData;

            m_SpriteRenderer.sprite = itemData.itemSprite;
        }

        public virtual void LateInitialize()
        {

        }

        public void Move(Vector3 position, float duration, Action onComplete = null)
        {
            transform.DOKill(true);
            transform.DOMove(position, duration)
                .OnComplete(() => onComplete?.Invoke());
        }

        protected virtual void OnClick()
        {

        }

        private void OnDragged()
        {
            if (m_CollidingCells.Count == 0)
            {
                CurrentCell.AddItem(this, transform.position, CurrentCell.transform.position);
                return;
            }

            Cell targetCell = m_CollidingCells[m_CollidingCells.Count - 1];

            if (targetCell.State == CellState.Unavailable || targetCell.State == CellState.Locked)
            {
                CurrentCell.AddItem(this, transform.position, CurrentCell.transform.position);
                return;
            }

            var mergingItems = MergeManager.TryGetMergeItems(this, targetCell.CurrentItem);

            if (mergingItems != null)
            {
                MergeManager.Merge(mergingItems, m_ItemPoolManager, m_GlobalGameData.allItemDatas);
            }
            else
            {
                if (targetCell.State == CellState.Occupied)
                {
                    targetCell.ShiftItem();
                }

                CurrentCell.RemoveItem();
                targetCell.AddItem(this, transform.position, targetCell.transform.position);
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

        private void OnDestroy()
        {
            /*
            m_ItemInputHandler.OnClick -= OnClick;
            m_ItemInputHandler.OnDragged -= OnDragged;*/
        }
    }
}