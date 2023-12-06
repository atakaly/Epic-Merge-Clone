using EpicMergeClone.Game.Items;
using EpicMergeClone.Pool;
using Zenject;

namespace EpicMergeClone.Installers
{
    public class PoolInstaller : Installer<PoolInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindMemoryPool<ItemBase, GenericItemPool<ItemBase>>()
                .WithInitialSize(15)
                .FromComponentInNewPrefabResource("Prefabs/ItemBase");

            Container.BindMemoryPool<CollectibleItem, GenericItemPool<CollectibleItem>>()
                .WithInitialSize(5)
                .FromComponentInNewPrefabResource("Prefabs/CollectibleItem");

            Container.BindMemoryPool<ProductionItem, GenericItemPool<ProductionItem>>()
                .WithInitialSize(2)
                .FromComponentInNewPrefabResource("Prefabs/ProductionItem");

            Container.Bind<ItemPoolManager>()
                .AsSingle();

            Container.QueueForInject(typeof(ItemPoolManager));
        }
    }
}