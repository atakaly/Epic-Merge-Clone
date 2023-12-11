using EpicMergeClone.Game.Mechanics.OrderSystem;
using UnityEngine;

namespace EpicMergeClone.Game.Items
{
    public class CharacterItem : ItemBase
    {
        public CharacterItemSO ItemData => ItemDataSO as CharacterItemSO;

        protected override void OnClick()
        {
            base.OnClick();

            m_UIManager.OrderPanel.Show();
            m_UIManager.OrderPanel.Initialize(m_OrderManager.GetCurrentOrderCharacterPairs());
            m_OrderManager.Initialize();
        }

        public void CompleteOrder()
        {
            ProduceOrderItems();
            ItemData.CompleteOrder();
        }

        private void ProduceOrderItems()
        {
            var currentOrder = GetCurrentOrder();

            var newCollectible = m_ItemPoolManager.SpawnItem(currentOrder.OrderItemSO);
            var cell = CurrentCell.GetFirstAvailableNeighbour();

            cell.AddItem(newCollectible, transform.position, cell.transform.position);
        }

        public Order GetCurrentOrder()
        {
            return m_GlobalGameData.allOrderDatas.GetOrder(ItemData.orders[ItemData.GetCurrentOrderIndex()].OrderId);
        }
    }
}