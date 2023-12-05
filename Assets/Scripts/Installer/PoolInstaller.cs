using EpicMergeClone.Game.Items;
using Zenject;

namespace EpicMergeClone.Installers
{
    public class PoolInstaller : Installer<PoolInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindMemoryPool<ItemBase, ItemPool<ItemBase>>()
                .WithInitialSize(15)
                .FromComponentInNewPrefabResource("Prefabs/ItemBase");

            Container.BindMemoryPool<CollectibleItem, ItemPool<CollectibleItem>>()
                .WithInitialSize(5)
                .FromComponentInNewPrefabResource("Prefabs/CollectibleItem");

            Container.BindMemoryPool<ProductionItem, ItemPool<ProductionItem>>()
                .WithInitialSize(2)
                .FromComponentInNewPrefabResource("Prefabs/ProductionItem");
        }
    }
}