using EpicMergeClone.Game.Mechanics;
using EpicMergeClone.Game.Mechanics.Board;
using EpicMergeClone.Game.Mechanics.Inventory;
using EpicMergeClone.UI;
using Zenject;

namespace EpicMergeClone.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            PoolInstaller.Install(Container);

            Container.Bind<UIManager>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.Bind<GameStateManager>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.QueueForInject(typeof(GameStateManager));

            Container.Bind<BoardManager>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.Bind<Inventory>()
                .AsSingle();

        }
    }
}