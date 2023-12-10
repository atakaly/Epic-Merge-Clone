using EpicMergeClone.Game.Items;
using EpicMergeClone.Game.Mechanics.InventorySystem;
using EpicMergeClone.Utils;
using System;
using UnityEngine;

namespace EpicMergeClone.Game.Mechanics.OrderSystem
{
    public class OrderManager
    {
        public Inventory m_Inventory;

        public OrderManager(Inventory inventory)
        {
            m_Inventory = inventory;
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

        public bool IsOrderCooked(Order order)
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
    }
}