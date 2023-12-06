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

        protected override void OnSpawned(T item)
        {
            base.OnSpawned(item);
            item.gameObject.SetActive(true);
        }

        protected override void OnCreated(T item)
        {
            base.OnSpawned(item);
            item.gameObject.SetActive(false);
        }

        protected override void OnDespawned(T item)
        {
            base.OnDespawned(item);
            item.gameObject.SetActive(false);
        }
    }
}