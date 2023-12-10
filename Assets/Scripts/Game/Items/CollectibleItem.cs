using EpicMergeClone.Game.Mechanics.InventorySystem;
using Zenject;

namespace EpicMergeClone.Game.Items
{
    public class CollectibleItem : ItemBase
    {
        public CollectibleItemSO ItemData => ItemDataSO as CollectibleItemSO;

        private Inventory m_Inventory;

        [Inject]
        public void Construct(Inventory inventory)
        {
            m_Inventory = inventory;
        }

        protected override void OnClick()
        {
            base.OnClick();
            Collect();
        }

        private void Collect()
        {
            m_Inventory.AddItem(this);
            m_ItemPoolManager.DespawnItem(this);
        }
    }
}