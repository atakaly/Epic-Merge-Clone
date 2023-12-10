using EpicMergeClone.Game.Mechanics.Board;
using EpicMergeClone.Game.Mechanics.OrderSystem;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace EpicMergeClone.UI.OrderUI
{
    public class OrderPanel : PopUpPanel
    {
        [SerializeField] private OrderItemUI orderItemPrefab;
        [SerializeField] private OrderDetailsPanel orderDetailsPanelPrefab;

        [SerializeField] private RectTransform orderItemContainer;
        [SerializeField] private RectTransform orderDetailsContainer;

        private List<OrderUIItemsPair> orderUIItemsPairs;

        private BoardManager m_BoardManager;
        private OrderManager m_OrderManager;

        [Inject]
        public void Construct(BoardManager boardManager, OrderManager orderManager)
        {
            m_BoardManager = boardManager;
            m_OrderManager = orderManager;
        }

        public override void Start()
        {
            base.Start();
            orderUIItemsPairs = new List<OrderUIItemsPair>();
        }

        public void Initialize()
        {
            var currentOrders = m_BoardManager.GetCurrentOrders();

            ClearUIItems();
            PopulateUIItems(currentOrders);
        }

        public void PopulateUIItems(List<BoardManager.CharacterOrderPair> currentCharOrderPairs)
        {
            foreach (var orderPair in currentCharOrderPairs)
            {
                OrderUIItemsPair pair = TryGetPanels();
                pair.Order = orderPair.Order;
                pair.ItemUI.Initialize(orderPair.Order);
                pair.DetailsPanel.Initialize(orderPair.Order, m_OrderManager, orderPair.characterItemSO);

                orderUIItemsPairs.Add(pair);
            }
        }

        private OrderUIItemsPair TryGetPanels()
        {
            OrderUIItemsPair pair = orderUIItemsPairs.Find(item => !item.IsActive);

            if (pair == null)
            {
                var orderItemUI = Instantiate(orderItemPrefab, orderItemContainer);
                var newDetailsPanel = Instantiate(orderDetailsPanelPrefab, orderDetailsContainer);

                pair = new OrderUIItemsPair()
                {
                    ItemUI = orderItemUI,
                    DetailsPanel = newDetailsPanel
                };
            }

            pair.SetActive(true);
            return pair;
        }

        private void ClearUIItems()
        {
            foreach (var pair in orderUIItemsPairs)
            {
                pair.SetActive(false);
            }
        }

        public void CompleteOrder()
        {

        }

        [System.Serializable]
        public class OrderUIItemsPair
        {
            public Order Order;
            public OrderItemUI ItemUI;
            public OrderDetailsPanel DetailsPanel;
            public bool IsActive;

            public void SetActive(bool active)
            {
                IsActive = active;
                ItemUI.gameObject.SetActive(active);
                DetailsPanel.gameObject.SetActive(active);
            }
        }
    }
}