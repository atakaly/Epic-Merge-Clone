using EpicMergeClone.Game.Mechanics.OrderSystem;
using System.Collections.Generic;
using UnityEngine;

namespace EpicMergeClone.UI.OrderUI
{
    public class OrderPanel : PopUpPanel
    {
        [SerializeField] private OrderItemUI orderItemPrefab;
        [SerializeField] private OrderDetailsPanel orderDetailsPanelPrefab;

        [SerializeField] private RectTransform orderItemContainer;
        [SerializeField] private RectTransform orderDetailsContainer;

        private List<OrderUIItemsPair> orderUIItemsPairs = new List<OrderUIItemsPair>();

        public void Initialize(List<OrderManager.CharacterOrderPair> characterOrderPairs)
        {
            ClearUIItems();
            PopulateUIItems(characterOrderPairs);
        }

        public void PopulateUIItems(List<OrderManager.CharacterOrderPair> currentCharOrderPairs)
        {
            foreach (var orderPair in currentCharOrderPairs)
            {
                OrderUIItemsPair pair = TryGetPanels();
                pair.Order = orderPair.Order;
                pair.ItemUI.Initialize(orderPair.Order);
                pair.DetailsPanel.Initialize(orderPair.Order, orderPair.characterItem.ItemData);

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

        public OrderDetailsPanel GetOrderDetailsPanel(Order order)
        {
            foreach (var pair in orderUIItemsPairs)
            {
                if (pair.Order == order)
                    return pair.DetailsPanel;
            }

            return null;
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