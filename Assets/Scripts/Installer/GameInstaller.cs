using EpicMergeClone.Game.Mechanics.Inventory;
using Zenject;

namespace EpicMergeClone.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            PoolInstaller.Install(Container);

            Container.Bind<Inventory>().AsSingle();
        }
    }
}