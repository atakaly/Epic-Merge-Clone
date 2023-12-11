using EpicMergeClone.Game.Items;
using EpicMergeClone.Game.Mechanics.Board;
using EpicMergeClone.Game.Mechanics.InventorySystem;
using EpicMergeClone.UI;
using EpicMergeClone.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EpicMergeClone.Game.Mechanics.OrderSystem
{
    public class OrderManager
    {
        private BoardManager m_BoardManager;
        private UIManager m_UIManager;
        private Inventory m_Inventory;

        public List<CharacterOrderPair> m_CurrentOrderCharacterPairs;

        public OrderManager(BoardManager boardManager, UIManager uiManager, Inventory inventory)
        {
            m_Inventory = inventory;
            m_UIManager = uiManager;
            m_BoardManager = boardManager;
        }

        public void Initialize()
        {
            m_CurrentOrderCharacterPairs = GetCurrentOrderCharacterPairs();

            foreach (var orderCharacterPairs in m_CurrentOrderCharacterPairs)
            {
                var detailsPanel = m_UIManager.OrderPanel.GetOrderDetailsPanel(orderCharacterPairs.Order);
                detailsPanel.OnCookClicked.AddListener((order) => CookOrder(order));
                detailsPanel.OnClaimClicked.AddListener((order) => ClaimOrder(order));

                detailsPanel.SetCookButtonClickable(IsOrderCookable(orderCharacterPairs.Order));
                detailsPanel.SetButtonsVisibility(IsOrderCooked(orderCharacterPairs.Order));
            }
        }

        public List<CharacterOrderPair> GetCurrentOrderCharacterPairs()
        {
            List<CharacterOrderPair> characterOrderPairs = new List<CharacterOrderPair>();
            List<CharacterItem> characters = m_BoardManager.FindCharacterItems();

            for (int i = 0; i < characters.Count; i++)
            {
                characterOrderPairs.Add(new CharacterOrderPair()
                {
                    characterItem = characters[i],
                    Order = characters[i].GetCurrentOrder()
                });
            }

            return characterOrderPairs;
        }

        public void CookOrder(Order order)
        {
            if (IsOrderCookable(order))
            {
                string prefName = Order.ORDER_PREF_NAME_PREFIX + order.OrderId;
                PlayerPrefsStorage.SetDateTime(prefName, DateTime.Now);

                foreach (var ingredient in order.OrderIngredients)
                {
                    m_Inventory.RemoveItem(ingredient.Item as CollectibleItemSO, ingredient.Count);
                }
            }
            else
            {
                Debug.Log("Insufficient ingredients");
            }
        }

        private void ClaimOrder(Order order)
        {
            if (!IsOrderCooked(order))
                return;

            var pair = m_CurrentOrderCharacterPairs.Find(pair => pair.Order == order);
            pair.characterItem.CompleteOrder();

            m_CurrentOrderCharacterPairs = GetCurrentOrderCharacterPairs();
            m_UIManager.OrderPanel.Initialize(m_CurrentOrderCharacterPairs);
            m_UIManager.OrderPanel.Hide();
        }

        private bool IsOrderCooked(Order order)
        {
            string prefName = Order.ORDER_PREF_NAME_PREFIX + order.OrderId;

            if (PlayerPrefs.HasKey(prefName))
            {
                DateTime startTime = PlayerPrefsStorage.GetDateTime(prefName, DateTime.Now);
                DateTime currentTime = DateTime.Now;

                TimeSpan elapsed = currentTime - startTime;

                return elapsed.TotalSeconds >= order.OrderCompletionTimeInSec;
            }

            return false;
        }

        public bool IsOrderCookable(Order order)
        {
            foreach (var ingredient in order.OrderIngredients)
            {
                if (ingredient.Count > m_Inventory.GetItemCount(ingredient.Item.ItemId))
                    return false;
            }

            return true;
        }


        [System.Serializable]
        public struct CharacterOrderPair
        {
            public CharacterItem characterItem;
            public Order Order;
        }
    }
}