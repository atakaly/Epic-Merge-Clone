using EpicMergeClone.Game.Items;
using Zenject;

namespace EpicMergeClone.Pool
{
    public class ItemPoolManager
    {
        private GenericItemPool<MergeItem> m_MergeItemPool;
        private GenericItemPool<CollectibleItem> m_CollectibleItemPool;
        private GenericItemPool<IngredientProducerItem> m_IngredientProductionItemPool;
        private GenericItemPool<CharacterItem> m_CharacterItemPool;
        private GenericItemPool<OrderItem> m_OrderItemPool;
        private GenericItemPool<ProductionItem> m_ProductionItemPool;
        

        [Inject]
        public void Construct(GenericItemPool<MergeItem> mergeItemPool, GenericItemPool<CollectibleItem> collectibleItemPool, 
            GenericItemPool<IngredientProducerItem> ingredientProductionItemPool, GenericItemPool<CharacterItem> characterItemPool,
            GenericItemPool<OrderItem> orderItemPool, GenericItemPool<ProductionItem> productionItemPool)
        {
            m_MergeItemPool = mergeItemPool;
            m_CollectibleItemPool = collectibleItemPool;
            m_IngredientProductionItemPool = ingredientProductionItemPool;
            m_ProductionItemPool = productionItemPool;
            m_CharacterItemPool = characterItemPool;
            m_OrderItemPool = orderItemPool;
        }

        public ItemBase SpawnItem(ItemBase item)
        {
            ItemBase newItem = null;

            if (item is IngredientProducerItem)
            {
                newItem = m_IngredientProductionItemPool.Spawn();
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
            else if (item is ProductionItem)
            {
                newItem = m_ProductionItemPool.Spawn();
            }
            else if (item is MergeItem)
            {
                newItem = m_MergeItemPool.Spawn();
            }

            return newItem;
        }

        public ItemBase SpawnItem(ItemDataSO itemData)
        {
            ItemBase newItem = null;

            if (itemData is IngredientProducerItemSO)
            {
                newItem = m_IngredientProductionItemPool.SpawnItem(itemData);
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
            else if (itemData is ProductionItemSO)
            {
                newItem = m_ProductionItemPool.SpawnItem(itemData);
            }
            else if (itemData is ItemDataSO)
            {
                newItem = m_MergeItemPool.SpawnItem(itemData);
            }

            return newItem;
        }

        public void DespawnItem(ItemBase item)
        {
            item.CurrentCell.RemoveItem();

            if (item is IngredientProducerItem ingredientProductionItem)
            {
                m_IngredientProductionItemPool.Despawn(ingredientProductionItem);
            }
            else if (item is ProductionItem productionItem)
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
            else if (item is MergeItem mergeItem)
            {
                m_MergeItemPool.Despawn(mergeItem);
            }
        }
    }
}