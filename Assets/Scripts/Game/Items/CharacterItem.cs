using EpicMergeClone.Game.Mechanics.OrderSystem;
using EpicMergeClone.UI.OrderUI;
using UnityEngine;

namespace EpicMergeClone.Game.Items
{
    public class CharacterItem : ItemBase
    {
        public CharacterItemSO ItemData => ItemDataSO as CharacterItemSO;

        protected override void OnClick()
        {
            base.OnClick();

            FindObjectOfType<OrderPanel>().Show();
        }

        public void OrderComplete()
        {
            int currentOrderIndex = ItemData.GetCurrentOrderIndex();
            currentOrderIndex++;
            if(currentOrderIndex >= ItemData.orders.Count - 1)
            {
                currentOrderIndex = ItemData.orders.Count - 1;
            }

            PlayerPrefs.SetInt(ItemData.ItemId + CharacterItemSO.CURRENT_ORDER_PREF_SUFFIX, currentOrderIndex);
        }

        public Order GetCurrentOrder()
        {
            return m_GlobalGameData.allOrderDatas.GetOrder(ItemData.orders[ItemData.GetCurrentOrderIndex()].OrderId);
        }
    }
}