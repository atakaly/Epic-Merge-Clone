using EpicMergeClone.Game.Items;
using UnityEngine;
using Zenject;

namespace EpicMergeClone.Installers
{
    [CreateAssetMenu(menuName = "Global Game Data")]
    public class GlobalGameData : ScriptableObjectInstaller<GlobalGameData>
    {
        public AllItemDatas allItemDatas;

        public override void InstallBindings()
        {
            Container.BindInstance(allItemDatas);
        }
    }
}