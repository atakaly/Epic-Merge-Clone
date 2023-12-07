using EpicMergeClone.Game.Mechanics.Order;
using System.Collections.Generic;
using UnityEngine;

namespace EpicMergeClone.Game.Items
{
    [CreateAssetMenu(menuName = "Item/New Character Item")]
    public class CharacterItemSO : ItemDataSO
    {
        public const string CURRENT_ORDER_PREF_SUFFIX = "_CurrentOrderIndex";

        public List<OrderGroup> orders;

        public int GetCurrentOrderIndex()
        {
            return PlayerPrefs.GetInt(ItemId + CURRENT_ORDER_PREF_SUFFIX);
        }
    }
}