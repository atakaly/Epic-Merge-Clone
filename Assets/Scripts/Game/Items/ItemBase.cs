using DG.Tweening;
using EpicMergeClone.Game.Mechanics;
using EpicMergeClone.Game.Mechanics.Board;
using EpicMergeClone.Game.Mechanics.Grid;
using EpicMergeClone.Game.Mechanics.OrderSystem;
using EpicMergeClone.Installers;
using EpicMergeClone.Pool;
using EpicMergeClone.UI;
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

        [SerializeField] public Cell CurrentCell { get; set; }

        protected ItemPoolManager m_ItemPoolManager;
        protected GlobalGameData m_GlobalGameData;
        protected GameStateManager m_GameStateManager;
        protected OrderManager m_OrderManager;
        protected UIManager m_UIManager;

        [Inject]
        public void Construct(ItemPoolManager itemPoolManager,
            GlobalGameData globalGameData,
            GameStateManager gameStateManager,
            OrderManager orderManager,
            UIManager UIManager)
        {
            m_ItemPoolManager = itemPoolManager;
            m_GlobalGameData = globalGameData;
            m_GameStateManager = gameStateManager;
            m_OrderManager = orderManager;
            m_UIManager = UIManager;
        }

        protected virtual void Start()
        {
            m_ItemInputHandler = GetComponent<ItemInputHandler>();

            m_ItemInputHandler.OnClick += OnClick;
            m_ItemInputHandler.OnEndDrag += OnDragEnd;
            m_ItemInputHandler.OnStartDrag += OnDragStarted;
        }

        private void OnEnable()
        {
            m_CollidingCells.Clear();
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

        private void OnDragStarted()
        {
            CurrentCell.RemoveItem();
        }

        private void OnDragEnd()
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

            var mergingItems = MergeManager.GetMergeItems(this, targetCell.CurrentItem);

            if (mergingItems != null && mergingItems.Count != 0)
            {
                MergeManager.Merge(mergingItems, m_ItemPoolManager, m_GlobalGameData.allItemDatas);
            }
            else
            {
                if (targetCell.State == CellState.Occupied)
                {
                    targetCell.ShiftItem();
                }

                targetCell.AddItem(this, transform.position, targetCell.transform.position);
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

        private void OnDisable()
        {
            CurrentCell = null;
        }
    }
}