using EpicMergeClone.Game.Mechanics.OrderSystem;
using System.Collections.Generic;
using UnityEngine;
using static EpicMergeClone.GameState;

namespace EpicMergeClone.Game.Items
{
    [CreateAssetMenu(menuName = "Item/New Character Item")]
    public class CharacterItemSO : ItemDataSO
    {
        public const string CURRENT_ORDER_PREF_SUFFIX = "_CurrentOrderIndex";

        public List<Order> orders;

        public int GetCurrentOrderIndex()
        {
            return PlayerPrefs.GetInt(ItemId + CURRENT_ORDER_PREF_SUFFIX);
        }

        public void CompleteOrder()
        {
            int currentOrderIndex = GetCurrentOrderIndex();
            currentOrderIndex++;
            if (currentOrderIndex >= orders.Count - 1)
            {
                currentOrderIndex = orders.Count - 1;
            }

            PlayerPrefs.SetInt(ItemId + CURRENT_ORDER_PREF_SUFFIX, currentOrderIndex);
        }
    }
}