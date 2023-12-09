using EpicMergeClone.Game.Items;
using Zenject;
using static UnityEditor.Progress;

namespace EpicMergeClone.Pool
{
    public class ItemPoolManager
    {
        private GenericItemPool<ItemBase> m_BaseItemPool;
        private GenericItemPool<CollectibleItem> m_CollectibleItemPool;
        private GenericItemPool<IngredientProducerItem> m_ProductionItemPool;
        private GenericItemPool<CharacterItem> m_CharacterItemPool;
        private GenericItemPool<OrderItem> m_OrderItemPool;
        

        [Inject]
        public void Construct(GenericItemPool<ItemBase> baseItemPool, GenericItemPool<CollectibleItem> collectibleItemPool, 
            GenericItemPool<IngredientProducerItem> productionItemPool, GenericItemPool<CharacterItem> characterItemPool,
            GenericItemPool<OrderItem> orderItemPool)
        {
            m_BaseItemPool = baseItemPool;
            m_CollectibleItemPool = collectibleItemPool;
            m_ProductionItemPool = productionItemPool;
            m_CharacterItemPool = characterItemPool;
            m_OrderItemPool = orderItemPool;
        }

        public ItemBase SpawnItem(ItemBase item)
        {
            ItemBase newItem;

            if (item is IngredientProducerItem)
            {
                newItem = m_ProductionItemPool.Spawn();
            }
            else if (item is CollectibleItem)
            {
                newItem = m_CollectibleItemPool.Spawn();
            }
            else if (item is OrderItem)
            {
                newItem = m_OrderItemPool.Spawn();
            }
            else if (item is CharacterItem)
            {
                newItem = m_CharacterItemPool.Spawn();
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

            if (itemData is IngredientProducerItemSO)
            {
                newItem = m_ProductionItemPool.SpawnItem(itemData);
            }
            else if (itemData is CollectibleItemSO)
            {
                newItem = m_CollectibleItemPool.SpawnItem(itemData);
            }
            else if (itemData is OrderItemSO)
            {
                newItem = m_OrderItemPool.SpawnItem(itemData);
            }
            else if (itemData is CharacterItemSO)
            {
                newItem = m_CharacterItemPool.SpawnItem(itemData);
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

            if (item is IngredientProducerItem productionItem)
            {
                m_ProductionItemPool.Despawn(productionItem);
            }
            else if (item is CollectibleItem collectibleItem)
            {
                m_CollectibleItemPool.Despawn(collectibleItem);
            }
            else if (item is OrderItem orderItem)
            {
                m_OrderItemPool.Despawn(orderItem);
            }
            else if (item is CharacterItem characterItem)
            {
                m_CharacterItemPool.Despawn(characterItem);
            }
            else
            {
                m_BaseItemPool.Despawn(item);
            }
        }
    }
}