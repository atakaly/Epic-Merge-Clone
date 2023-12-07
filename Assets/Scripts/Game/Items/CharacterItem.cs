using EpicMergeClone.Game.Mechanics.Order;
using UnityEngine;

namespace EpicMergeClone.Game.Items
{
    public class CharacterItem : ItemBase
    {
        public CharacterItemSO ItemData => ItemDataSO as CharacterItemSO;

        protected override void OnMouseDown()
        {
            base.OnMouseDown();

            // if order done, create order.
            // if no order open UI
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

        public OrderGroup GetCurrentOrder()
        {
            return m_GlobalGameData.allOrderDatas.GetOrderGroup(ItemData.orders[ItemData.GetCurrentOrderIndex()].OrderId);
        }
    }
}