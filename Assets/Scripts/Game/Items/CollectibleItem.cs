using EpicMergeClone.Game.Mechanics.Board;
using EpicMergeClone.Game.Mechanics.InventorySystem;
using Zenject;

namespace EpicMergeClone.Game.Items
{
    public class CollectibleItem : ItemBase
    {
        public CollectibleItemSO ItemData => ItemDataSO as CollectibleItemSO;

        private Inventory m_Inventory;
        private BoardManager m_BoardManager;

        [Inject]
        public void Construct(Inventory inventory, BoardManager boardManager)
        {
            m_Inventory = inventory;
            m_BoardManager = boardManager;
        }

        protected override void OnClick()
        {
            base.OnClick();
            Collect();
        }

        private void Collect()
        {
            var collectibles = m_BoardManager.FindCollectibleItems();

            for (int i = 0; i < collectibles.Count; i++)
            {
                m_Inventory.AddItem(collectibles[i]);
                m_ItemPoolManager.DespawnItem(collectibles[i]);
            }
        }
    }
}