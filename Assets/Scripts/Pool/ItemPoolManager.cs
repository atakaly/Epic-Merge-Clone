using EpicMergeClone.Game.Items;
using Zenject;

namespace EpicMergeClone.Pool
{
    public class ItemPoolManager
    {
        private GenericItemPool<ItemBase> m_BaseItemPool;
        private GenericItemPool<CollectibleItem> m_CollectibleItemPool;
        private GenericItemPool<ProductionItem> m_ProductionItemPool;

        [Inject]
        public void Construct(GenericItemPool<ItemBase> baseItemPool, GenericItemPool<CollectibleItem> collectibleItemPool, GenericItemPool<ProductionItem> productionItemPool)
        {
            m_BaseItemPool = baseItemPool;
            m_CollectibleItemPool = collectibleItemPool;
            m_ProductionItemPool = productionItemPool;
        }

        public ItemBase SpawnItem(ItemBase item)
        {
            ItemBase newItem;

            if (item is ProductionItem)
            {
                newItem = m_ProductionItemPool.Spawn();
            }
            else if (item is CollectibleItem)
            {
                newItem = m_CollectibleItemPool.Spawn();
            }
            else
            {
                newItem = m_BaseItemPool.Spawn();
            }

            return newItem;
        }

        public ItemBase SpawnItem(ItemDataSO itemData)
        {
            ItemBase newItem;

            if (itemData is ProductionItemSO)
            {
                newItem = m_ProductionItemPool.SpawnItem(itemData);
            }
            else if (itemData is CollectibleItemSO)
            {
                newItem = m_CollectibleItemPool.SpawnItem(itemData);
            }
            else
            {
                newItem = m_BaseItemPool.SpawnItem(itemData);
            }

            return newItem;
        }

        public void DespawnItem(ItemBase item)
        {
            item.CurrentCell.RemoveItem();

            if (item is ProductionItem productionItem)
            {
                m_ProductionItemPool.Despawn(productionItem);
            }
            else if (item is CollectibleItem collectibleItem)
            {
                m_CollectibleItemPool.Despawn(collectibleItem);
            }
            else
            {
                m_BaseItemPool.Despawn(item);
            }
        }
    }
}