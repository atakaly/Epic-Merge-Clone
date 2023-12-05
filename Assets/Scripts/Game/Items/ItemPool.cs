using Zenject;

namespace EpicMergeClone.Game.Items
{
    public class ItemPool<T> : MemoryPool<T> where T : ItemBase
    {
        public T SpawnItem(ItemDataSO itemData)
        {
            T item = Spawn();
            item.InitializeItem(itemData);
            return item;
        }
    }
}