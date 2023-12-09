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

        private Dictionary<OrderItemUI, OrderDetailsPanel> orderItemDetailsPairs;

        private List<OrderDetailsPanel> orderDetailsPanels;

        private BoardManager m_BoardManager;

        [Inject]
        public void Construct(BoardManager boardManager)
        {
            m_BoardManager = boardManager;
        }

        public override void Start()
        {
            base.Start();

            Initialize();
        }

        public void Initialize()
        {
            orderItemDetailsPairs = new Dictionary<OrderItemUI, OrderDetailsPanel>();

            var currentOrders = m_BoardManager.GetCurrentOrders();

            PopulateUIItems(currentOrders);
        }

        public void PopulateUIItems(List<Order> currentOrders)
        {
            foreach (var order in currentOrders)
            {
                var orderItemUI = Instantiate(orderItemPrefab, orderItemContainer);
                orderItemUI.Initialize(order);

                var newDetailsPanel = Instantiate(orderDetailsPanelPrefab, orderDetailsContainer);
                newDetailsPanel.Initialize(order, null);

                orderItemDetailsPairs.Add(orderItemUI, newDetailsPanel);
            }
        }

        public void CompleteOrder()
        {

        }
    }
}