using EpicMergeClone.Game.Items;
using Zenject;

namespace EpicMergeClone.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            PoolInstaller.Install(Container);
        }
    }
}