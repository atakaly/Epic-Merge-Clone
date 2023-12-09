using EpicMergeClone.Game.Mechanics;
using EpicMergeClone.Game.Mechanics.Board;
using EpicMergeClone.Game.Mechanics.Inventory;
using Zenject;

namespace EpicMergeClone.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            PoolInstaller.Install(Container);

            Container.Bind<BoardManager>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.Bind<Inventory>()
                .AsSingle();

            Container.Bind<GameStateManager>()
                .AsSingle();
        }
    }
}