using EpicMergeClone.Game.Mechanics;
using EpicMergeClone.Game.Mechanics.Board;
using EpicMergeClone.Game.Mechanics.InventorySystem;
using EpicMergeClone.Game.Mechanics.OrderSystem;
using EpicMergeClone.UI;
using Zenject;

namespace EpicMergeClone.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            PoolInstaller.Install(Container);

            Container.BindInterfacesAndSelfTo<UIManager>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<GameStateManager>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<BoardManager>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<OrderManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<Inventory>().AsSingle();
        }
    }
}